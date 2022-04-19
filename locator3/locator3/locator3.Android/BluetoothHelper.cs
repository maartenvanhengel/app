using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AndroidX.Activity.Result.Contract.ActivityResultContracts;

namespace locator3.Droid
{
    
    public class BluetoothHelper //: IBluetoothHelper
    {
        BluetoothManager bluetoothManager ;
        BluetoothAdapter bluetoothAdapter;
        
        public void Getconnection()
        {
            bluetoothAdapter = bluetoothManager.Adapter;
            if (bluetoothAdapter == null)
            {
                //geen bluetooth
            }
        }
        public void EnableBluetooth()
        {
            if (bluetoothAdapter.IsEnabled == false)
            {
                Intent enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                //   StartActivityForResult(enableBtIntent, REQUEST_ENABLE_BT)
            }
        }
        private void Paireddevices()
        {
            ICollection<BluetoothDevice> pairedDevices = bluetoothAdapter.BondedDevices;

            if (pairedDevices.Count() > 0)
            {
                // There are paired devices. Get the name and address of each paired device.
                foreach (BluetoothDevice device in pairedDevices)
                {
                    string deviceName = device.Name;
                    string deviceAdress = device.Address; //mac address
                }
            }
        }
        


    }
}