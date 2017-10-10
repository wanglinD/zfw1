using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Service.Entities;

namespace ZF.Service
{
    //一些公共的方法
    //泛型约束
    class BaseService<T> where T:BaseEntity
    {
        private MyDbContext ctx;
        public BaseService(MyDbContext ctx)
        {
            this.ctx = ctx;
        
        }
        /// <summary>
        /// 获取所有没有软删除的数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return ctx.Set<T>().Where(e => e.IsDeleted == false);
        }

        /// <summary>
        /// 获取数据的总条数
        /// </summary>
        /// <returns></returns>
        public long GetTotalCount()
        {
            return GetAll().LongCount();
        }
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="startIndex">跳过几条数据</param>
        /// <param name="count">获取条数</param>
        /// <returns></returns>
        public IQueryable<T> GetPagedData(int startIndex,int count)
        {
            //把的到的数据，按照时间排序，跳过starIndex条数据，得到最多count条数据
            return GetAll().OrderBy(e => e.CreateDateTime).Skip(startIndex).Take(count);
        }

        /// <summary>
        /// 查找Id=id 的数据，如果没有返回null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(long id)
        {
            return GetAll().Where(e=>e.Id==id).SingleOrDefault();
        }
        /// <summary>
        /// 软删除数据，根据id查出数据，更改对象的IsDeleted属性为true
        /// </summary>
        /// <param name="id"></param>
        public void MarkDeleted(long id)
        {
            var data = GetById(id);
            data.IsDeleted = true;
            ctx.SaveChanges();
        }
    }
}
