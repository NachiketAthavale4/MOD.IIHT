using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MOD.TrainingService.Contexts;
using MOD.TrainingService.Models;

namespace MOD.TrainingService.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly TrainingContext trainingContext;

        public TrainingRepository(TrainingContext trainingContext)
        {
            this.trainingContext = trainingContext;
        }

        public bool Delete(int id)
        {
            try
            {
                var trainingsFound = trainingContext.Trainings.Find(id);
                if (trainingsFound != null)
                {
                    trainingContext.Trainings.Remove(trainingsFound);
                    trainingContext.SaveChanges();
                    if (trainingContext.Trainings.Find(id) == null)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Training> GetAllTrainings()
        {
            try
            {
                var trainingsFound = trainingContext.Trainings.ToList();
                return trainingsFound;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Training GetTrainingById(int id)
        {
            try
            {
                var trainingFound = trainingContext.Trainings.Find(id);
                return trainingFound;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Save(Training training)
        {
            try
            {
                trainingContext.Trainings.Add(training);
                return trainingContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Update(Training training)
        {
            try
            {
                var updateObj = trainingContext.Trainings.Attach(training);
                updateObj.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return trainingContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
