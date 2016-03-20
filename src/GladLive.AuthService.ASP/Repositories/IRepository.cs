using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladLive.AuthService.ASP
{
	/// <summary>
	/// Repository interface for queries for <typeparamref name="TModelType"/> generic models.
	/// </summary>
	/// <typeparam name="TModelType">The model type.</typeparam>
	public interface IRepository<TModelType>
		where TModelType : class
	{
		/// <summary>
		/// Queries for the model instance by the id.
		/// </summary>
		/// <param name="id">Model id.</param>
		/// <returns>The model instance or null if not found.</returns>
		TModelType Get(int id);

		/// <summary>
		/// Queries for the existence of the model by the id.
		/// </summary>
		/// <param name="id">Model id.</param>
		/// <returns>True if the model exists or false otherwise.</returns>
		bool Exists(int id);

		/// <summary>
		/// Queries for a non-lazy collection of all <typeparamref name="TModelType"/> models.
		/// </summary>
		/// <returns>A non-lazy non-null collection of all <typeparamref name="TModelType"/>s.</returns>
		IEnumerable<TModelType> GetAll();
	}

	/// <summary>
	/// Repository interface for async queries for <typeparamref name="TModelType"/> generic models.
	/// </summary>
	/// <typeparam name="TModelType">The model type</typeparam>
	public interface IRepositoryAsync<TModelType>
		where TModelType : class
	{
		/// <summary>
		/// Async queries for the model instance by the id.
		/// </summary>
		/// <param name="id">Model id.</param>
		/// <returns>A future model instance or null if not found.</returns>
		Task<TModelType> GetAsync(int id);

		/// <summary>
		/// Async queries for the existence of the model by the id.
		/// </summary>
		/// <param name="id">Model id.</param>
		/// <returns>A future bool of True if the model exists or false otherwise.</returns>
		Task<bool> ExistsAsync(int id);

		/// <summary>
		/// Async queries for a non-lazy collection of all <typeparamref name="TModelType"/> models.
		/// </summary>
		/// <returns>A non-lazy non-null future collection of all <typeparamref name="TModelType"/>s.</returns>
		Task<IEnumerable<TModelType>> GetAllAsync();
	}
}
