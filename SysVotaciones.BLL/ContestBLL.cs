﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysVotaciones.DAL;
using SysVotaciones.EN;

namespace SysVotaciones.BLL
{
    public class ContestBLL
    {
        private readonly ContestDAL _contestDAL;

        public ContestBLL (string conn)
        {
            _contestDAL = new ContestDAL(conn);
        }

        public List<Contest> GetAll() => _contestDAL.GetContests();

        public Contest? GeById(int id)
        {
            Contest? contest = _contestDAL.GetContest(id);
            return contest;
        }

        public int Save(Contest contest)
        {
            var contestToEvalueate = new
            {
                name = contest.Name,
                description = contest.Description,
            };

            bool isValid = Helper.AllPropertiesHaveValue(contestToEvalueate);
            
            if (!isValid) return 0;

            int rowsAffected = _contestDAL.SaveContest(contest);
            return rowsAffected;
        }

        public int Delete(int id)
        {
            int rowsAffected = _contestDAL.DeleteContest(id);
            return rowsAffected;
        }

        public int Update(Contest contest)
        {
            if (contest.Id == default) return 0;

            int rowsAffected = _contestDAL.UpdateContest(contest);
            return rowsAffected;
        }
    }
}
