﻿using Domain.Entities;

namespace mvcapp.Models.Activities
{
    public class ActivityViewModel
    {
        public Activity Activity { get; set; }

        public CompleteActivityViewModel CompleteActivity { get; set; }
    }
}
