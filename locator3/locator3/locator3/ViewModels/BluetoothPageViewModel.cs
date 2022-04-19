using locator3.Repositories;
using Plugin.BluetoothClassic.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Mail;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace locator3.ViewModels
{
    public class BluetoothPageViewModel : ViewModelBase
    {
        IBluetoothHelper bluetoothHelper;
        IPageDialogService pageDialogService;
        public BluetoothPageViewModel(INavigationService navigationService, IBluetoothHelper bluetoothHelper, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            this.bluetoothHelper = bluetoothHelper;
            this.pageDialogService = pageDialogService;

            connectCommand = new DelegateCommand(executeConnect);
        }

           public ICommand connectCommand { get; private set; }
           public ObservableCollection<BluetoothDeviceModel> Items { get; private set; }

           private async void executeConnect()
           {

            bluetoothHelper.Getconnection();
             /*  const int BufferSize = 1;
               const int OffsetDefault = 0;

               byte value = 1;


               if (device != null)
               {
               //    var _bluetoothAdapter = DependencyService.Resolve<IBluetoothAdapter>();


                   var connection = bluetoothAdapter.CreateConnection(device);
                  await connection.ConnectAsync();
                       if (await connection.RetryConnectAsync(retriesCount: 5))
                       {
                           byte[] buffer = new byte[BufferSize] { value };
                           try
                           {
                               if (!await connection.RetryTransmitAsync(
                                       buffer, OffsetDefault, buffer.Length))
                               {
                                   await pageDialogService.DisplayAlertAsync("Error", "Can not transmit data.", "Close");
                               }
                           }
                           catch (Exception exception)
                           {
                               await pageDialogService.DisplayAlertAsync("Error", exception.Message, "Close");
                           }
                       }
                       else
                       {
                           await pageDialogService.DisplayAlertAsync("Error", "Can  to connect.", "Close");
                       }

               }  */

           }
      /*     private BluetoothDeviceModel selectedItem;
           public BluetoothDeviceModel SelectedItem
           {
               get { return selectedItem; }
               set {if (SetProperty(ref selectedItem, value))
                   {
                     //  device = SelectedItem;
                   }
               ; }
           }

           public void checkBluetoothDevices()
           {
            //   var bluetoothAdapter = DependencyService.Resolve<IBluetoothAdapter>();
           //    var items = bluetoothAdapter.BondedDevices;

           }

           public byte[] GetImageStreamAsBytes(Stream input)
           {
               var buffer = new byte[16 * 1024];
               using (MemoryStream ms = new MemoryStream())
               {
                   int read;
                   while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                   {
                       ms.Write(buffer, 0, read);
                   }
                   return ms.ToArray();
               }
           }
        */
    }
}
