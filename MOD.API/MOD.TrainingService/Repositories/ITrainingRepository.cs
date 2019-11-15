using MOD.TrainingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOD.TrainingService.Repositories
{
    public interface ITrainingRepository
    {
        int Save(Training training);
        int Update(Training training);
        bool Delete(int id);
        IEnumerable<Training> GetAllTrainings();
        Training GetTrainingById(int id);
    }
}
