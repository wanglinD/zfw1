using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Service.Entities;

namespace ZF.Service
{
    //泛型约束
    class BaseService<T> where T:BaseEntity
    {
        private MyDbContext ctx;
        public BaseService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }

        /// <summary>
        /// 获取没有软删除的数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return ctx.Set<T>().Where(e => e.IsDeleted == false);

        }

        /// <summary>
        /// 获取总数据条数
        /// </summary>
        /// <returns></returns>
        public long GetTotalCount()
        {
            return GetAll().LongCount();
            
        }

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IQueryable<T> GetPagedData(int startIndex,int count)
        {
            //分页前要orderBy排序，跳过startIndex条数据，获取最多count条数据
            return GetAll().OrderBy(e => e.CreateDateTime)
                .Skip(startIndex).Take(count);
        }

        /// <summary>
        /// 查找id=id的数据，如果找不到返回null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(long id)
        {
            return GetAll().Where(e => e.Id == id).SingleOrDefault();
        }

        /// <summary>
        /// 根据id软删除数据
        /// </summary>
        /// <param name="id"></param>
        public void MarkDeleted(long id)
        {
            //先查数据
            var data = GetById(id);
            data.IsDeleted = true;
            ctx.SaveChanges();
        }
    }
}
