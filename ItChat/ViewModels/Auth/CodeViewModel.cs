using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ItChat.ViewModels.Auth
{
    public class CodeViewModel : BaseViewModel
    {

        private string code = string.Empty;

        public string Code
        {
            get => this.code;
            set {
                if (this.code == string.Empty || this.code.Length < 4)
                {
                    base.SetProperty<string>(ref this.code, value);
                }
                else
                {
                    base.SetProperty<string>(ref this.code, string.Empty);
                }

                base.OnPropertyChanged(nameof(IsFullCode));
                ((Command)this.ContinueCommand).ChangeCanExecute();
            }
        }

        public bool IsFullCode
        {
            get => this.code.Length < 4;
        }

        public ICommand ResendCodeCommand { get; private set; }
        public ICommand ContinueCommand { get; private set; }

        public CodeViewModel()
        {
            this.ResendCodeCommand = new Command(async () => await this.ResendCode());
            this.ContinueCommand = new Command(async () => await this.Continue(), () => !this.IsFullCode);
        }

        private async Task ResendCode()
        {
            await Shell.Current.DisplayAlert("Code", this.code.ToString(), "Ok");
            this.Code = string.Empty;
        }

        private async Task Continue()
        {
            await Shell.Current.DisplayAlert("Continue", $"Successfull {this.code}", "Ok");
        }
    }
}
