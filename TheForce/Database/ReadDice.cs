using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using TheForce.Dice;
using TheForce.Face;
using TheForce.Character;
using TheForce.Sets;
namespace TheForce.Dice
{
    public class ReadDice
    {
        readonly SQLiteAsyncConnection _database;

        public ReadDice(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Die>().Wait();
            _database.CreateTableAsync<RollDie>().Wait();
            _database.CreateTableAsync<FacePool>().Wait();
            _database.CreateTableAsync<DieFace>().Wait();
            _database.CreateTableAsync<Skill>().Wait();
            _database.CreateTableAsync<Att>().Wait();
            _database.CreateTableAsync<SkillDice>().Wait();
            _database.CreateTableAsync<AttDice>().Wait();
            _database.CreateTableAsync<Set>().Wait();
            _database.CreateTableAsync<ActiveSet>().Wait();
        }

        #region ActiveSet
        public Task<List<ActiveSet>> GetActiveSetAsync()
        {
            return _database.Table<ActiveSet>().ToListAsync();
        }

        public Task<int> SaveActiveSetAsync(ActiveSet set)
        {
            _database.DropTableAsync<DieFace>().Wait();
            _database.CreateTableAsync<DieFace>().Wait();

            return _database.InsertAsync(set);
        }
        #endregion

        #region sets
        public Task<List<Set>> GetSetAsync()
        {
            return _database.Table<Set>().ToListAsync();
        }

        public Task<Set> GetSetAsync(int id)
        {
            return _database.Table<Set>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveSetAsync(Set set)
        {
            if (set.ID != 0)
            {
                return _database.UpdateAsync(set);
            }
            else
            {
                return _database.InsertAsync(set);
            }
        }

        public Task<int> DeleteSetAsync(Set set)
        {
            return _database.DeleteAsync(set);
        }
        #endregion

        #region skill and att
        public Task<SkillDice> GetSkillDieAsync()
        {
            return _database.Table<SkillDice>()
                            .Where(i => i.ID == 1)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveSkillDieAsync(SkillDice die)
        {
            if (die.ID != 1)
            {
                return _database.UpdateAsync(die);
            }
            else
            {
                return _database.InsertAsync(die);
            }
        }

        public Task<AttDice> GetAttDieAsync()
        {
            return _database.Table<AttDice>()
                            .Where(i => i.ID == 1)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveAttDieAsync(AttDice die)
        {
            if (die.ID != 1)
            {
                return _database.UpdateAsync(die);
            }
            else
            {
                return _database.InsertAsync(die);
            }
        }
        #endregion

        public Task<List<Die>> GetDieAsync()
        {
            return _database.Table<Die>().ToListAsync();
        }

        public Task<List<RollDie>> GetRollDieAsync()
        {
            return _database.Table<RollDie>().ToListAsync();
        }

        public Task<Die> GetDieAsync(int id)
        {
            return _database.Table<Die>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<RollDie> GetRollDieAsync(int id)
        {
            return _database.Table<RollDie>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveDieAsync(Die die)
        {
            if (die.ID != 0)
            {
                return _database.UpdateAsync(die);
            }
            else
            {
                return _database.InsertAsync(die);
            }
        }

        public Task<int> SaveRollDieAsync(RollDie die)
        {
            if (die.ID != 0)
            {
                return _database.UpdateAsync(die);
            }
            else
            {
                return _database.InsertAsync(die);
            }
        }

        public Task<int> DeleteDieAsync(Die die)
        {
            return _database.DeleteAsync(die);
        }

        public Task<int> DeleteRollDieAsync(RollDie rolldie)
        {
            return _database.DeleteAsync(rolldie);
        }

        public void ResetRollDieAsync()
        {
            _database.DropTableAsync<RollDie>().Wait();
            _database.CreateTableAsync<RollDie>().Wait();
        }

        #region facepool
        public Task<List<FacePool>> GetFaceAsync()
        {
            return _database.Table<FacePool>().ToListAsync();
        }

        public Task<FacePool> GetFaceAsync(int id)
        {
            return _database.Table<FacePool>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveFaceAsync(FacePool face)
        {
            if (face.ID != 0)
            {
                return _database.UpdateAsync(face);
            }
            else
            {
                return _database.InsertAsync(face);
            }
        }

        public Task<int> DeleteFaceAsync(FacePool face)
        {
            return _database.DeleteAsync(face);
        }
        #endregion

        #region diefacepool
        public Task<List<DieFace>> GetDieFaceAsync()
        {
            return _database.Table<DieFace>().ToListAsync();
        }

        public Task<DieFace> GetDieFaceAsync(int id)
        {
            return _database.Table<DieFace>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveDieFaceAsync(DieFace face)
        {
            if (face.ID != 0)
            {
                return _database.UpdateAsync(face);
            }
            else
            {
                return _database.InsertAsync(face);
            }
        }

        public Task<int> DeleteDieFaceAsync(DieFace face)
        {
            return _database.DeleteAsync(face);
        }

        public void ResetDieFaceAsync()
        {
            _database.DropTableAsync<DieFace>().Wait();
            _database.CreateTableAsync<DieFace>().Wait();
        }
        #endregion

        #region skills
        public Task<List<Skill>> GetSkillAsync()
        {
            return _database.Table<Skill>().ToListAsync();
        }

        public Task<Skill> GetSkillAsync(int id)
        {
            return _database.Table<Skill>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveSkillAsync(Skill skill)
        {
            if (skill.ID != 0)
            {
                return _database.UpdateAsync(skill);
            }
            else
            {
                return _database.InsertAsync(skill);
            }
        }

        public Task<int> DeleteSkillAsync(Skill skill)
        {
            return _database.DeleteAsync(skill);
        }
        #endregion

        #region attributes
        public Task<List<Att>> GetAttAsync()
        {
            return _database.Table<Att>().ToListAsync();
        }

        public Task<Att> GetAttAsync(int id)
        {
            return _database.Table<Att>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveAttAsync(Att att)
        {
            if (att.ID != 0)
            {
                return _database.UpdateAsync(att);
            }
            else
            {
                return _database.InsertAsync(att);
            }
        }

        public Task<int> DeleteAttAsync(Att att)
        {
            return _database.DeleteAsync(att);
        }
        #endregion
    }
}
