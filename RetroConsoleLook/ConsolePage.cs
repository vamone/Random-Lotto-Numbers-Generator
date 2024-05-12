using System.ComponentModel;

namespace RetroConsoleLook
{
    public class ConsolePage : INotifyPropertyChanged
    {
        int _index = 0;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ConsolePage(string title, List<string> items)
        {
            this.Title = title;
            this.Items = items;
        }

        public string Title { get; set; }

        public List<string> Items { get; set; }

        public int Index
        {
            get { return this._index; }
            set
            {
                if (this._index != value)
                {
                    this._index = value;
                    this.OnPropertyChanged(nameof(this.Index));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}