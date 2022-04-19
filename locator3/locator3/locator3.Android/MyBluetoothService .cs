using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace locator3.Droid
{
    class MyBluetoothService
    {
        private static String TAG = "MY_APP_DEBUG_TAG";
        private Handler handler; // handler that gets info from Bluetooth service

        // Defines several constants used when transmitting messages between the
        // service and the UI.
        private interface MessageConstants
        {
            public static int MESSAGE_READ = 0;
            public static int MESSAGE_WRITE = 1;
            public static int MESSAGE_TOAST = 2;
        }

        // ... (Add other message types here as needed.)
        private class ConnectedThread //extends Thread
        {
            private BluetoothSocket mmSocket;
            private System.IO.Stream mmInStream;
            private System.IO.Stream mmOutStream;
            private byte[] mmBuffer; // mmBuffer store for the stream



            public ConnectedThread(BluetoothSocket socket)
            {
                mmSocket = socket;
                System.IO.Stream tmpIn = null;
                System.IO.Stream tmpOut = null;

                // Get the input and output streams; using temp objects because
                // member streams are final.
                try
                {
                    tmpIn = socket.InputStream;
                }
                catch (IOException e)
                {

                    //   Lg.e(TAG, "Error occurred when creating input stream", e);
                }
                try
                {
                    tmpOut = socket.OutputStream;
                }
                catch (IOException e)
                {
                    //   Log.e(TAG, "Error occurred when creating output stream", e);
                }

                mmInStream = tmpIn;
                mmOutStream = tmpOut;
            }

        public void run()
        {
            byte[] mmBuffer = new byte[1024];
            int numBytes; // bytes returned from read()

            // Keep listening to the InputStream until an exception occurs.
            while (true)
            {
                try
                {
                    // Read from the InputStream.
                    numBytes = mmInStream.Read(mmBuffer);
                    // Send the obtained bytes to the UI activity.
                  
                        /*  Message readMsg = Handler.obtainMessage(
                            MessageConstants.MESSAGE_READ, numBytes, -1,
                            mmBuffer);  */
                   // readMsg.sendToTarget();
                }
                catch (IOException e)
                {
                  //  Log.d(TAG, "Input stream was disconnected", e);
                    break;
                }
            }
        }
            public void write(byte[] bytes)
            {
                try
                {
                    mmOutStream.Write(bytes);

                    // Share the sent message with the UI activity.
                  /*  Message writtenMsg = handler.obtainMessage(
                            MessageConstants.MESSAGE_WRITE, -1, -1, mmBuffer);
                    writtenMsg.sendToTarget();  */
                }
                catch (IOException e)
                {
                   /* Log.e(TAG, "Error occurred when sending data", e);

                    // Send a failure message back to the activity.
                    Message writeErrorMsg =
                            handler.obtainMessage(MessageConstants.MESSAGE_TOAST);
                    Bundle bundle = new Bundle();
                    bundle.putString("toast",
                            "Couldn't send data to the other device");
                    writeErrorMsg.setData(bundle);
                    handler.sendMessage(writeErrorMsg);  */
                }
            }

            // Call this method from the main activity to shut down the connection.
            public void cancel()
            {
                try
                {
                    mmSocket.Close();
                }
                catch (IOException e)
                {
                  //  Log.e(TAG, "Could not close the connect socket", e);
                }
            }

        }
    }
}