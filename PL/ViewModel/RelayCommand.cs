﻿using System;
using System.Windows.Input;

namespace FourSeasons.ViewModel
{
    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged   //вызывается при изменении условий, указывающих, может ли команда выполняться
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)   //определяет, может ли команда выполняться
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)      //выполняет логику команды
        {
            execute(parameter);
        }
    }
}
