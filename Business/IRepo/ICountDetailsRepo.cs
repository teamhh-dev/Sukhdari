﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepo
{
    public interface ICountDetailsRepo
    {
        public void addClick(int storeId);
        public Task<IEnumerable<StoreDTO>> getWeeklyTopStores();
    }
}
