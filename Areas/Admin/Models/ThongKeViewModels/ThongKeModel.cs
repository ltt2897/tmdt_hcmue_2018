using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PhuKienDienThoai.Areas.Admin.Models.ThongKeViewModels
{
    public class ThongKeModel
    {
        public List<SelectListItem> Thang { get; set; }
        public List<SelectListItem> Nam { get; set; }
        public int _month { get; set; } //tháng hiện tại
        public int _year { get; set; }//năm hiện tại
        public ThongKeModel()
        {
            _month = DateTime.Now.Month;
            _year = DateTime.Now.Year;
            Thang = new List<SelectListItem>();
            Nam = new List<SelectListItem>();
            for (int thang = 1; thang <= 12; thang++)
            {
                Thang.Add(new SelectListItem
                {
                    Value = thang.ToString(),
                    Text = thang.ToString(),
                });
            }
            for (int nam = 2017; nam <= _year; nam++)
            {
                Nam.Add(new SelectListItem
                {
                    Value = nam.ToString(),
                    Text = nam.ToString()
                });
            }
        }
    }
}