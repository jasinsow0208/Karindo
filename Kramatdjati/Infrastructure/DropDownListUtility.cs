using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kramatdjati.Infrastructure
{
    public static class DropDownListUtility
    {
        public static IEnumerable<SelectListItem> StatusPosting(Object selectedValue)
        {
            return new List<SelectListItem> {new SelectListItem {Text="Belum Posting",Value="0", Selected ="0" == selectedValue.ToString()},
                                             new SelectListItem {Text="Posting",Value="1", Selected ="1" == selectedValue.ToString()}
            };
        }

        public static IEnumerable<SelectListItem> PeriodDropDown(object selectedValue)
        {
            return new List<SelectListItem>
                                    {
                                        new SelectListItem{ Text="Januari", Value = "1", Selected = "1" == selectedValue.ToString() },
                                        new SelectListItem{ Text="Pebruari", Value = "2", Selected = "2" == selectedValue.ToString() },
                                        new SelectListItem{ Text="Maret", Value = "3" , Selected = "3" == selectedValue.ToString()},
                                        new SelectListItem{ Text="April", Value = "4", Selected = "4" == selectedValue.ToString() },
                                        new SelectListItem{ Text="Mei", Value = "5" , Selected = "5" == selectedValue.ToString()},
                                        new SelectListItem{ Text="Juni", Value = "6" , Selected = "6" == selectedValue.ToString()},
                                        new SelectListItem{ Text="Juli", Value = "7" , Selected = "7" == selectedValue.ToString()},
                                        new SelectListItem{ Text="Agustus", Value = "8", Selected = "8" == selectedValue.ToString() },
                                        new SelectListItem{ Text="September", Value = "9" , Selected = "9" == selectedValue.ToString()},
                                        new SelectListItem{ Text="Oktober", Value = "10", Selected = "10" == selectedValue.ToString() },
                                        new SelectListItem{ Text="Nopember", Value = "11" , Selected = "11" == selectedValue.ToString()},
                                        new SelectListItem{ Text="Desember", Value = "12" , Selected = "12" == selectedValue.ToString()}
                                   };
        }
        public static IEnumerable<SelectListItem> YearDropDown(object selectedValue)
        {
            return new List<SelectListItem>
                                    {
                                        new SelectListItem{ Text="2015", Value = "2015", Selected = "2015" == selectedValue.ToString()},
                                        new SelectListItem{ Text="2016", Value = "2016", Selected = "2016" == selectedValue.ToString()},
                                        new SelectListItem{ Text="2017", Value = "2017", Selected = "2017" == selectedValue.ToString()},
                                        new SelectListItem{ Text="2018", Value = "2018", Selected = "2018" == selectedValue.ToString()},
                                        new SelectListItem{ Text="2019", Value = "2019", Selected = "2019" == selectedValue.ToString()},
                                        new SelectListItem{ Text="2020", Value = "2020", Selected = "2020" == selectedValue.ToString()},
                                        new SelectListItem{ Text="2021", Value = "2021", Selected = "2021" == selectedValue.ToString()},
                                        new SelectListItem{ Text="2022", Value = "2022", Selected = "2022" == selectedValue.ToString()},
                                        new SelectListItem{ Text="2023", Value = "2023", Selected = "2023" == selectedValue.ToString()},
                                        new SelectListItem{ Text="2024", Value = "2024", Selected = "2024" == selectedValue.ToString()},
                                        new SelectListItem{ Text="2025", Value = "2025", Selected = "2025" == selectedValue.ToString()},
                                        new SelectListItem{ Text="2026", Value = "2026", Selected = "2026" == selectedValue.ToString()}
                                   };
        }

        public static IEnumerable<SelectListItem> LaporanKeuangan(object selectedValue)
        {
            return new List<SelectListItem>
                                    {
                                        new SelectListItem{ Text="Profit & Loss", Value = "1", Selected = "1" == selectedValue.ToString()},
                                        new SelectListItem{ Text="Balance Sheet", Value = "2", Selected = "2" == selectedValue.ToString()}
                                   };
        }
    }
}