﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MyWebSite.DAL.Repositories.Abstract
{
    //referans type constraint . T yi class a sabitleme başka birşey olamaz.
    public interface IBaseRepository<T> where T : class
   {
       int AddItem(T item);
       int DeleteItem(T item);
       int UpdateITem(T item);
       T GetItem(Expression<Func<T,bool>> lambda=null);

       ICollection<T> GetAllItem(Expression<Func<T, bool>> lambda = null);

   }
}
