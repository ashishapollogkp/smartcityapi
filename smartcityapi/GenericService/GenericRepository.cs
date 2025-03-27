using Microsoft.EntityFrameworkCore;
using smartcityapi.Context;
using System;

namespace smartcityapi.GenericService
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		


		private readonly SmartCityDBContext _context;
		private readonly DbSet<T> _dbSet;

		public GenericRepository(SmartCityDBContext context)
		{
			_context = context;
			_dbSet = context.Set<T>();
		}


		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<T?> GetByIdAsync(long id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task<T> AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task<T> UpdateAsync(T entity)
		{
			_dbSet.Update(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task<bool> DeleteAsync(long id)
		{
			var entity = await _dbSet.FindAsync(id);
			if (entity == null) return false;

			_dbSet.Remove(entity);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
