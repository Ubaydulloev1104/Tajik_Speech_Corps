using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Sieve
{
	public interface IApplicationSieveProcessor : ISieveProcessor
	{
		PagedList<TResult> ApplyAdnGetPagedList<TSource, TResult>(SieveModel model, IQueryable<TSource> source,
			Func<TSource, TResult> converter);
	}
}
