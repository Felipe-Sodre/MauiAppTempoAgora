using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao =    $"Lat: {t.lat}\n" +
                                            $"Lon: {t.lon}\n" +
                                            $"Temp Min: {t.temp_min} °C\n" +
                                            $"Temp Max: {t.temp_max} °C\n" +
                                            $"Nascer do Sol: {t.sunrise}\n" +
                                            $"Pôr do Sol: {t.sunset}\n" +
                                            $"\nADICIONADOS\n"  +
                                            $"Visibilidade: {t.visibility} m\n" +//Adicionado
                                            $"Velocidade do Vento: {t.speed} m/s\n" +//Adicionado
                                            $"Descrição: {t.description}\n" + //Adicionado
                                            $"Clima: {t.main}\n"; //Alem

                                            lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        lbl_res.Text = "Cidade não encontrada, tente novamente"; //Mensagem alterada
                    }

                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }

            }
            catch (Exception ex)
            {
                //await DisplayAlert("Erro", "Verifique sua conecção com a internet", "OK");
                await DisplayAlert("🚫 Sem Internet", "Verifique sua conexão e tente novamente.", "OK");


            }
        }

    }
}