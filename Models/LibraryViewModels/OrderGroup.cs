using System;
using System.ComponentModel.DataAnnotations;

namespace Bruj_Tudor_Lab2_EB.Models.LibraryViewModels
{
    public class OrderGroup
    {
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }
        public int BookCount { get; set; }

    }
}
