﻿using System.Collections.Generic;
using SFA.DAS.ApprenticeProgress.Application.Models;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetTaskCategoriesResult : QueryResult<GetTaskCategoriesResult>
    {
        public List<Domain.Entities.ApprenticeshipCategory> TaskCategories { get; set; }
    }
}