using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AP.BOL.Service.Services
{
    public static class FunctionCompositionExtensions
    {
        public static Expression<Func<TX, TY>> Compose<TX, TY, TZ>(this Expression<Func<TZ, TY>> outer, Expression<Func<TX, TZ>> inner)
        {
            return Expression.Lambda<Func<TX, TY>>(
                ParameterReplacer.Replace(outer.Body, outer.Parameters[0], inner.Body),
                inner.Parameters[0]);
        }
    }
}
