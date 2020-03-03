namespace KnightCrawler.Engine
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Implements the basic functionality of the INotifyPropertyChanged interface.
    /// </summary>
    public class BaseNotificationClass : INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fires the PropertyChanged event for the caller.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
