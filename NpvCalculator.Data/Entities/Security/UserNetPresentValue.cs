﻿using System;

namespace NpvCalculator.Data.Entities
{
    public class UserNetPresentValue
    {
        public int UserNetPresentValueId { get; set; }
        public Guid UserId { get; set; }
        public int NetPresentValueId { get; set; }
    }
}