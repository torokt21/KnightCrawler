using KnightCrawler.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KnightCrawler.Wpf.Pages
{
  public class BasePage<VM> : Page
        where VM : GameViewModel

    {
        /// <summary>
        /// The View Model associated with this page
        /// </summary>
        private GameViewModel mViewModel;

        public object ViewModelObject
        {
            get => this.mViewModel;
            set
            {
                // If nothing has changed, return
                if (this.mViewModel == value)
                {
                    return;
                }

                // Update the value
                //this.mViewModel = value;

                // Set the data context for this page
                this.DataContext = this.mViewModel;
            }
        }
    }
}
