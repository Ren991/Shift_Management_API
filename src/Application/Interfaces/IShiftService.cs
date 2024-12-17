﻿using Application.Models.BarberShopDtos;
using Application.Models.ShiftDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IShiftService
    {
        List<Shift> GetAllShift();

        ShiftDto AddNewShift(ShiftCreateRequest shiftDto);

        void ConfirmShift(int shiftId, int clientId, IEnumerable<int>? serviceIds, bool payShift);
    }
}
