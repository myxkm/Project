using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MongoDBUtils.Models
{
    public static class MongodbExpansion
    {
        /// <summary>
        /// 单个对象添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="entity"></param>
        public static void Add<T>(this IMongoCollection<T> collection, T entity) where T : IAggregateRoot
            => collection.InsertOne(entity);

        /// <summary>
        /// 集合添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="entitys"></param>
        public static void AddRange<T>(this IMongoCollection<T> collection, List<T> entitys) where T : IAggregateRoot
            => collection.InsertMany(entitys);


        /// <summary>
        /// entity mongodb需要更新的实体 properts需要更新的集合属性,大小写不限 默认更新整个对象 replace 默认空属性不修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="entity">mongodb需要更新的实体</param>
        /// <param name="properts">需要更新的集合属性,大小写不限 默认更新整个对象 </param>
        /// <param name="replace">默认对象属性为空时不替换当前值</param>
        public static void Update<T>(this IMongoCollection<T> collection, T entity, List<string> properts = null, bool replace = false) where T : IAggregateRoot
        {
            if (entity == null)
                throw new NullReferenceException();

            var type = entity.GetType();
            ///修改的属性集合
            var list = new List<UpdateDefinition<T>>();

            foreach (var propert in type.GetProperties())
            {
                if (propert.Name.ToLower() != "id")
                {
                    if (properts == null || properts.Count < 1 || properts.Any(o => o.ToLower() == propert.Name.ToLower()))
                    {
                        var replaceValue = propert.GetValue(entity);
                        if (replaceValue != null)
                        {
                            list.Add(Builders<T>.Update.Set(propert.Name, replaceValue));
                        }
                        else if (replace)
                            list.Add(Builders<T>.Update.Set(propert.Name, replaceValue));
                    }
                }
            }
            #region 有可修改的属性
            if (list.Count > 0)
            {
                ///合并多个修改//new List<UpdateDefinition<T>>() { Builders<T>.Update.Set("Name", "111") }
                var builders = Builders<T>.Update.Combine(list);
                ///执行提交修改
                collection.UpdateOne(o => o.Id == entity.Id, builders);
            }
            #endregion

        }

        /// <summary>
        /// 移除根据ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="entity"></param>
        public static void Remove<T>(this IMongoCollection<T> collection, T entity) where T : IAggregateRoot
        => collection.DeleteOne(o => o.Id == entity.Id);

        /// <summary>
        /// 移除表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="expression"></param>
        public static void Remove<T>(this IMongoCollection<T> collection, Expression<Func<T, bool>> expression) where T : IAggregateRoot
        => collection.DeleteOne(expression);

        public static IFindFluent<T, T> Where<T>(this IMongoCollection<T> collection, Expression<Func<T, bool>> expression=null)
        => expression == null ? collection.Find(Builders<T>.Filter.Empty) : collection.Find(expression);

        /// <summary>
        /// MongoDB里面是没有实现IQueryable的 所以想要使用习惯的SELECT我们只能自己封装一下
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <typeparam name="TProjection"></typeparam>
        /// <typeparam name="TNewProjection">返回新的数据类型</typeparam>
        /// <param name="IQueryable"></param>
        /// <param name="projection"></param>
        /// <returns></returns>
        public static IFindFluent<TDocument, TNewProjection> Select<TDocument, TProjection, TNewProjection>(this IFindFluent<TDocument, TProjection> IQueryable, Expression<Func<TDocument, TNewProjection>> projection)
        => IQueryable.Project(projection);


        /// <summary>
        /// 获得筛选后的首个元素
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="IQueryable"></param>
        /// <returns></returns>
        public static T FirstOrDefault<TDocument, T>(this IFindFluent<TDocument, T> IQueryable)
         => IQueryable.First();


        /// <summary>
        /// 直接支持表达式树后的首个满足对象
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static T FirstOrDefault<TDocument, T>(this IMongoCollection<T> collection, Expression<Func<T, bool>> expression = null)
        => expression == null ? collection.Find(Builders<T>.Filter.Empty).First() : collection.Find(expression).First();
    }
}