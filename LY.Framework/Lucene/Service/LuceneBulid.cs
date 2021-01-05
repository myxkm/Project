using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using LuceneIO = Lucene.Net.Store;
using LuceneUtil = Lucene.Net.Util;
using LY.Framework.Lucene.Interface;
using LY.Framework.Config;
using LY.Framework.LoggerHelper;

namespace LY.Framework.Lucene.Service
{
    /// <summary>
    /// 多线程的问题 ：多文件写，然后合并
    /// 延时：异步队列
    /// 
    /// </summary>
    public class LuceneBulid : ILuceneBulid
    {
        #region Identity
        private Logger logger = Logger.CreateLogger(typeof(LuceneBulid));
        #endregion Identity
        #region 批量BuildIndex 索引合并
        /// <summary>
        /// 批量创建索引(要求是统一的sourceflag，即目录是一致的)
        /// </summary>
        /// <param name="ciList">sourceflag统一的</param>
        /// <param name="pathSuffix">索引目录后缀，加在电商的路径后面，为空则为根目录.如sa\1</param>
        /// <param name="isCreate">默认为false 增量索引  true的时候删除原有索引</param>
        public void BuildIndex<T>(List<T> ciList, string pathSuffix = "", bool isCreate = false)
        {
            IndexWriter writer = null;
            try
            {
                if (ciList == null || ciList.Count == 0)
                {
                    return;
                }
                string rootIndexPath = StaticConstant.IndexPath;
                string indexPath = string.IsNullOrWhiteSpace(pathSuffix) ? rootIndexPath : string.Format("{0}\\{1}", rootIndexPath, pathSuffix);

                DirectoryInfo dirInfo = Directory.CreateDirectory(indexPath);
                LuceneIO.Directory directory = LuceneIO.FSDirectory.Open(dirInfo);
                writer = new IndexWriter(directory, new PanGuAnalyzer(), isCreate, IndexWriter.MaxFieldLength.LIMITED);
                //writer = new IndexWriter(directory, CreateAnalyzerWrapper(), isCreate, IndexWriter.MaxFieldLength.LIMITED);
                writer.SetMaxBufferedDocs(100);//控制写入一个新的segent前内存中保存的doc的数量 默认10  
                writer.MergeFactor = 100;//控制多个segment合并的频率，默认10
                writer.UseCompoundFile = true;//创建复合文件 减少索引文件数量

                ciList.ForEach(c => CreateCIIndex(writer, c));
            }
            finally
            {
                if (writer != null)
                {
                    //writer.Optimize(); 创建索引的时候不做合并  merge的时候处理
                    //writer.Close();
                    writer.Dispose();
                }
            }
        }
        /// <summary>
        /// 将索引合并到上级目录
        /// </summary>
        /// <param name="sourceDir">子文件夹名</param>
        public void MergeIndex(string[] childDirs)
        {
            Console.WriteLine("MergeIndex Start");
            IndexWriter writer = null;
            try
            {
                if (childDirs == null || childDirs.Length == 0) return;
                Analyzer analyzer = new StandardAnalyzer(LuceneUtil.Version.LUCENE_30);
                string rootPath = StaticConstant.IndexPath;
                DirectoryInfo dirInfo = Directory.CreateDirectory(rootPath);
                LuceneIO.Directory directory = LuceneIO.FSDirectory.Open(dirInfo);
                writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.LIMITED);//删除原有的
                LuceneIO.Directory[] dirNo = childDirs.Select(dir => LuceneIO.FSDirectory.Open(Directory.CreateDirectory(string.Format("{0}\\{1}", rootPath, dir)))).ToArray();
                writer.MergeFactor = 100;//控制多个segment合并的频率，默认10
                writer.UseCompoundFile = true;//创建符合文件 减少索引文件数量
                writer.AddIndexesNoOptimize(dirNo);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Optimize();
                    writer.Dispose();//.Close();
                }
                Console.WriteLine("MergeIndex End");
            }
        }
        //Field.Store.YES:存储字段值（未分词前的字段值）        
        //Field.Store.NO:不存储,存储与索引没有关系         
        //Field.Store.COMPRESS:压缩存储,用于长文本或二进制，但性能受损         
        //Field.Index.ANALYZED:分词建索引         
        //Field.Index.ANALYZED_NO_NORMS:分词建索引，但是Field的值不像通常那样被保存，而是只取一个byte，这样节约存储空间         
        //Field.Index.NOT_ANALYZED:不分词且索引         
        //Field.Index.NOT_ANALYZED_NO_NORMS:不分词建索引，Field的值去一个byte保存         
        //TermVector表示文档的条目（由一个Document和Field定位）和它们在当前文档中所出现的次数         
        //Field.TermVector.YES:为每个文档（Document）存储该字段的TermVector         
        //Field.TermVector.NO:不存储TermVector         
        //Field.TermVector.WITH_POSITIONS:存储位置        
        //Field.TermVector.WITH_OFFSETS:存储偏移量         
        //Field.TermVector.WITH_POSITIONS_OFFSETS:存储位置和偏移量
        #endregion 批量BuildIndex 索引合并
        #region 单个/批量索引增删改
        /// <summary>
        /// 新增一条数据的索引
        /// </summary>
        /// <param name="t"></param>
        public void InsertIndex<T>(T t)
        {
            IndexWriter writer = null;
            try
            {
                if (t == null) return;
                string rootIndexPath = StaticConstant.IndexPath;
                DirectoryInfo dirInfo = Directory.CreateDirectory(rootIndexPath);

                bool isCreate = dirInfo.GetFiles().Count() == 0;//下面没有文件则为新建索引 
                LuceneIO.Directory directory = LuceneIO.FSDirectory.Open(dirInfo);
                writer = new IndexWriter(directory, CreateAnalyzerWrapper(), isCreate, IndexWriter.MaxFieldLength.LIMITED);
                writer.MergeFactor = 100;//控制多个segment合并的频率，默认10
                writer.UseCompoundFile = true;//创建符合文件 减少索引文件数量
                CreateCIIndex(writer, t);
            }
            catch (Exception ex)
            {
                logger.Error("InsertIndex异常", ex);
                throw ex;
            }
            finally
            {
                if (writer != null)
                {
                    //if (fileNum > 50)
                    //    writer.Optimize();
                    writer.Dispose();//writer.Close();
                }
            }
        }

        /// <summary>
        /// 批量新增数据的索引
        /// </summary>
        /// <param name="ciList"></param>
        public void InsertIndexMuti<T>(List<T> ciList)
        {
            BuildIndex(ciList, "", false);
        }

        /// <summary>
        /// 批量删除数据的索引
        /// </summary>
        /// <param name="ciList"></param>
        public void DeleteIndexMuti<T>(List<T> ciList)
        {
            IndexReader reader = null;
            try
            {
                if (ciList == null || ciList.Count == 0) return;
                Analyzer analyzer = new StandardAnalyzer(LuceneUtil.Version.LUCENE_30);
                string rootIndexPath = StaticConstant.IndexPath;
                DirectoryInfo dirInfo = Directory.CreateDirectory(rootIndexPath);
                LuceneIO.Directory directory = LuceneIO.FSDirectory.Open(dirInfo);
                reader = IndexReader.Open(directory, false);
                foreach (T t in ciList)
                {
                    //reader.DeleteDocuments(new Term("productid", t.ProductId.ToString()));
                }
            }
            catch (Exception ex)
            {
                logger.Error("DeleteIndex异常", ex);
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
            }
        }

        /// <summary>
        /// 删除多条数据的索引
        /// </summary>
        /// <param name="t"></param>
        public void DeleteIndex<T>(T t)
        {
            IndexReader reader = null;
            try
            {
                if (t == null) return;
                Analyzer analyzer = new StandardAnalyzer(LuceneUtil.Version.LUCENE_30);
                string rootIndexPath = StaticConstant.IndexPath;
                DirectoryInfo dirInfo = Directory.CreateDirectory(rootIndexPath);
                LuceneIO.Directory directory = LuceneIO.FSDirectory.Open(dirInfo);
                reader = IndexReader.Open(directory, false);
                //reader.DeleteDocuments(new Term("productid", t.ProductId.ToString()));
            }
            catch (Exception ex)
            {

                logger.Error("DeleteIndex异常", ex);
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
            }
        }

        ///// <summary>
        ///// 更新一条数据的索引
        ///// </summary>
        //public void UpdateIndex(Commodity t)
        //{
        //    DeleteIndex(t);
        //    InsertIndex(t);
        //}

        /// <summary>
        /// 更新一条数据的索引
        /// </summary>
        /// <param name="t"></param>
        public void UpdateIndex<T>(T t)
        {
            IndexWriter writer = null;
            try
            {
                if (t == null) return;
                string rootIndexPath = StaticConstant.IndexPath;
                DirectoryInfo dirInfo = Directory.CreateDirectory(rootIndexPath);

                bool isCreate = dirInfo.GetFiles().Count() == 0;//下面没有文件则为新建索引 
                LuceneIO.Directory directory = LuceneIO.FSDirectory.Open(dirInfo);
                writer = new IndexWriter(directory, CreateAnalyzerWrapper(), isCreate, IndexWriter.MaxFieldLength.LIMITED);
                writer.MergeFactor = 100;//控制多个segment合并的频率，默认10
                writer.UseCompoundFile = true;//创建符合文件 减少索引文件数量
                //writer.UpdateDocument(new Term("productid", t.ProductId.ToString()), ParseCItoDoc(t));
            }
            catch (Exception ex)
            {
                logger.Error("InsertIndex异常", ex);
                throw ex;
            }
            finally
            {
                if (writer != null)
                {
                    //if (fileNum > 50)
                    //    writer.Optimize();
                    writer.Dispose();//writer.Close();
                }
            }
        }

        /// <summary>
        /// 批量更新数据的索引
        /// </summary>
        /// <param name="tList">sourceflag统一的</param>
        public void UpdateIndexMuti<T>(List<T> tList)
        {
            IndexWriter writer = null;
            try
            {
                if (tList == null || tList.Count == 0) return;
                string rootIndexPath = StaticConstant.IndexPath;
                DirectoryInfo dirInfo = Directory.CreateDirectory(rootIndexPath);

                bool isCreate = dirInfo.GetFiles().Count() == 0;//下面没有文件则为新建索引 
                LuceneIO.Directory directory = LuceneIO.FSDirectory.Open(dirInfo);
                writer = new IndexWriter(directory, CreateAnalyzerWrapper(), isCreate, IndexWriter.MaxFieldLength.LIMITED);
                writer.MergeFactor = 50;//控制多个segment合并的频率，默认10
                writer.UseCompoundFile = true;//创建符合文件 减少索引文件数量
                foreach (T t in tList)
                {
                    //writer.UpdateDocument(new Term("productid", t.ProductId.ToString()), ParseCItoDoc(t));
                }
            }
            catch (Exception ex)
            {
                logger.Error("InsertIndex异常", ex);
                throw ex;
            }
            finally
            {
                if (writer != null)
                {
                    //if (fileNum > 50)
                    //    writer.Optimize();
                    writer.Dispose();//writer.Close();
                }
            }
        }
        #endregion 单个索引增删改
        #region PrivateMethod
        /// <summary>
        /// 创建分析器
        /// </summary>
        /// <returns></returns>
        private PerFieldAnalyzerWrapper CreateAnalyzerWrapper()
        {
            Analyzer analyzer = new StandardAnalyzer(LuceneUtil.Version.LUCENE_30);

            PerFieldAnalyzerWrapper analyzerWrapper = new PerFieldAnalyzerWrapper(analyzer);
            analyzerWrapper.AddAnalyzer("title", new PanGuAnalyzer());
            analyzerWrapper.AddAnalyzer("categoryid", new StandardAnalyzer(LuceneUtil.Version.LUCENE_30));
            return analyzerWrapper;
        }
        /// <summary>
        /// 创建索引
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="writer"></param>
        /// <param name="t"></param>
        private void CreateCIIndex<T>(IndexWriter writer, T t)
        {
            try
            {
                writer.AddDocument(ParseCItoDoc(t));
            }
            catch (Exception ex)
            {
                logger.Error("CreateIndex异常", ex);
                throw ex;
            }
        }
        /// <summary>
        /// 将t转换成doc
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        private Document ParseCItoDoc<T>(T t)
        {
            Document doc = new Document();

            //doc.Add(new Field("id", t.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            //doc.Add(new Field("title", t.Title, Field.Store.YES, Field.Index.ANALYZED));//盘古分词
            //doc.Add(new Field("productid", t.ProductId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            //doc.Add(new Field("categoryid", t.CategoryId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            //doc.Add(new Field("imageurl", t.ImageUrl, Field.Store.YES, Field.Index.NOT_ANALYZED));
            //doc.Add(new Field("url", t.Url, Field.Store.YES, Field.Index.NOT_ANALYZED));
            //doc.Add(new NumericField("price", Field.Store.YES, true).SetFloatValue((float)t.Price));
            return doc;
        }
        #endregion PrivateMethod
    }
}
