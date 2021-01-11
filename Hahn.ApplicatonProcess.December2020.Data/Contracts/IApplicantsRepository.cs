using Hahn.ApplicatonProcess.December2020.Domain.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Contracts
{
    public interface IApplicantsRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicant"></param>
        /// <returns></returns>
        Task<int> CreateApplicant(Applicant applicant);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Applicant> GetApplicant(int id);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Applicant>> GetApplicants();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="applicant"></param>
        /// <returns></returns>
        Task<Applicant> UpdateApplicant(int id, Applicant applicant);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Applicant> DeleteApplicant(int id);
    }
}