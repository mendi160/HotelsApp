using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BL;

namespace FinalGui
{
    /// <summary>
    /// Interaction logic for HostingUnitDetails.xaml
    /// </summary>
    public partial class HostingUnitDetails : Page
    {
        BO.HostingUnit HostingUnit { get; set; }
        BlApi.IBl bl { get; set; }
        List<CalendarDateRange> calendarDates;
        public HostingUnitDetails(BO.HostingUnit hostingUnit, BlApi.IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
            HostingUnit = hostingUnit;
            calendarDates = new List<CalendarDateRange>();
            setBlackOutDates(HostingUnit);
            MainGrid.DataContext = HostingUnit;
            OrdersDataGrid.ItemsSource = bl.GetOrdersOfHost(hostingUnit.Owner).Where(x => x.HostingUnit.Key == hostingUnit.Key && x.Status == BO.Order_Status.APPROVED);
            MonthlyProgressBar.Value = Math.Round(CalculateMonthlyPercentage(), 2);
            TotalProgressBar.Value = Math.Round(CalculateTotalPercentage(), 2);
        }

        private float CalculateMonthlyPercentage()
        {
            float totaldays = 0;

            foreach (var item in DatesCalendar.BlackoutDates.Where(x => x.End <= DateTime.Now.AddDays(30)))
            {
                totaldays += (item.End - item.Start).Days;
            }
            return (totaldays * 100 / 30);
        }

        private float CalculateTotalPercentage()
        {
            float totaldays = 0;
            foreach (var item in DatesCalendar.BlackoutDates)
            {
                totaldays += (item.End - item.Start).Days;
            }
            return totaldays * 100 / 365;
        }

        private void setBlackOutDates(BO.HostingUnit HostingUnit)
        {
            DateTime Start = new DateTime();
            DateTime End = new DateTime();
            bool XOR = false;
            DateTime temp = DateTime.Today;
            DateTime check = DateTime.Today;
            check = check.AddMonths(11);
            while (temp<check)
            {

                if (HostingUnit.GetDate(temp)^ XOR)
                {
                    if (!XOR)
                    {


                        Start = temp;
                        XOR = XOR ^ (!XOR);
                    }
                    else
                    {
                        End =temp;
                        XOR = XOR ^ XOR;
                        DatesCalendar.BlackoutDates.Add(new CalendarDateRange(Start, End));
                    }
                }
                temp = temp.AddDays(1);
            }

          }
    }
}
