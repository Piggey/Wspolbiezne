using System;
using System.Windows.Input;

namespace Prezentacja.ViewModel
{
    public sealed class RelayCommand : ICommand
    {
        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            this._mExecute = execute ?? throw new ArgumentNullException(nameof(execute));
            this._mCanExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (this._mCanExecute == null)
                return true;
            if (parameter == null)
                return this._mCanExecute();
            return this._mCanExecute();
        }

        public void Execute(object? parameter)
        {
            this._mExecute();
        }


        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private readonly Action _mExecute;
        private readonly Func<bool>? _mCanExecute;
    }
}