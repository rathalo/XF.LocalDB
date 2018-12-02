using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using XF.LocalDB.Model;

namespace XF.LocalDB.ViewModel
{
    public class UsuarioViewModel
    {
        public string Stream { get; set; }
        public Usuario UsuarioModel { get; set; }

        public Autenticar IsAutenticarCMD { get; set; }

        public UsuarioViewModel()
        {
            UsuarioModel = new Usuario();
            IsAutenticarCMD = new Autenticar(this);
            GetUsuario("http://apiaplicativofiap.azurewebsites.net/content/xml/usuarios.xml");
        }
        private async void GetUsuario(string paramURL)
        {
            var httpRequest = new HttpClient();
            Stream = await httpRequest.GetStringAsync(paramURL);
        }

        public void IsAutenticar(Usuario paramUser)
        {
            if (UsuarioRepository.IsAutenticado(paramUser))
            {
                App.Current.MainPage.Navigation.PushAsync(new View.Aluno.MainPage());
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Atenção", "Usuário não autorizado.", "OK");
            }
        }        
    }

    public class Autenticar : ICommand
    {
        UsuarioViewModel UsuarioVM;
        public Autenticar(UsuarioViewModel paramVM)
        {
            UsuarioVM = paramVM;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return (parameter != null);
        }

        public void Execute(object parameter)
        {
            UsuarioVM.IsAutenticar(parameter as Usuario);
        }
    }


}
