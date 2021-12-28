using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;
using System.Drawing.Imaging;

namespace BarCodeTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string tt = "";
        public string ss = "";
        public string ss1 = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        public object MessageBoxButtons { get; private set; }


        private static String ENCORD_NAME = "ISO-8859-1";
        
        private byte calcParity(byte[] data)
        {
            byte parity;

            parity = data[0];
            for (int i = 1; i < data.Length; i++)
                parity = (byte)(parity ^ data[i]);//XOR

            return parity;
        }
        

        private void btnLastTest2_Click(object sender, RoutedEventArgs e)
        {
            string ss = "9MA0GCSqGSIb3DQEBCwUABIIBABVim6AYRyf6AmkZFdAkKvaAvMkQEGYVwa2KUz/FBeu";
            byte parity = calcParity(Encoding.UTF8.GetBytes(ss));                // 全体のパリティ値
            int codesize = Convert.ToInt32(Math.Ceiling((double)ss.Length / 4)); // 個々のQRコードの文字列長

            var hints = new Dictionary<EncodeHintType, object>
            {
                [EncodeHintType.CHARACTER_SET] = "UTF-8",
                [EncodeHintType.ERROR_CORRECTION] = "L",
                [EncodeHintType.MARGIN] = 4,
                [EncodeHintType.QR_VERSION] = 8  // 何か相応しいバージョン(大きさ)を指定しておく
            };

            //Image[] images = { QrImage1, QrImage2, QrImage3, QrImage4 }; // あらかじめXAMLでImageを4つ作成しておき配列化

            QRCodeWriter qrWriter = new QRCodeWriter();  // QRCode作成用
            BarcodeWriter writer = new BarcodeWriter();  // Bitmapへの変換用

            for (int i = 0, iOffset = 0; i < 4; i++)
            {
                int length = Math.Min(codesize, ss.Length - iOffset);
                string codestr = ss.Substring(iOffset, length);  // 分割した文字列の切り出し
                iOffset += length;
                BitmapImage bitmapimage;
                // 200はQRCodeイメージの適当なサイズ、3は4つの連結QRCodeにする際の最終番号
                using (Bitmap bmp = writer.Write(qrWriter.encode_sa1(codestr, BarcodeFormat.QR_CODE, 200, 200, hints, i, 3, parity)))
                using (var ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Png);
                    // WPFで扱えるImageSourceに変換
                    //var source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    bitmapimage = new BitmapImage();
                    bitmapimage.BeginInit();
                    bitmapimage.StreamSource = ms;
                    bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapimage.EndInit();

                    if (i == 0)
                    {
                        imgQR1.Source = bitmapimage;
                    }
                    else if (i == 1)
                    {
                        imgQR2.Source = bitmapimage;
                    }
                    else if (i == 2)
                    {
                        imgQR3.Source = bitmapimage;
                    }
                    else if (i == 3)
                    {
                        imgQR4.Source = bitmapimage;
                    }
                }
            }
        }
    }





        
}




    //private void ReadBarCode_Click(object sender, RoutedEventArgs e)
    //{

    //    foreach (string items in lstAllImage.Items)
    //    {
    //        var qrcodebitmap = (Bitmap)Bitmap.FromFile(items);
    //        var qrcodeReader = new BarcodeReader();
    //        var qrcodeResult = qrcodeReader.Decode(qrcodebitmap);

    //        byte[] rawqrcode;
    //        rawqrcode = qrcodeResult.RawBytes;
    //        //MessageBox.Show(qrcodeResult.Text);
    //        ss = ss + qrcodeResult.Text;
    //        txtBarcode.Text = ss;
    //        // MessageBox.Show(qrcodeResult.BarcodeFormat.ToString());
    //    }



    //    //MessageBox.Show(filePath);


    //    //OpenFileDialog fileDialog = new OpenFileDialog();
    //    //fileDialog.Filter = "BarCode Files (*.bmp;*.BMP)|*.bmp;*.bmp|All files (*)|*.*";
    //    //Nullable<bool> dialogOK = fileDialog.ShowDialog();
    //    //openFileDialog.FilterIndex = 2;
    //    //openFileDialog.RestoreDirectory = true;
    //    //if (dialogOK == true)
    //    //{
    //    //    try
    //    //    {


    //    //    }
    //    //    catch(Exception ex)
    //    //    {

    //    //    }
    //    //}

    //}

//public void Convert_BitMap_BitImage_FromFolder_ForListView_ForDifferent(Bitmap bitImgs)
//{
//    using (MemoryStream memory = new MemoryStream())
//    {
//        bitImgs.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
//        memory.Position = 0;
//        BitmapImage bitmapimage = new BitmapImage();
//        bitmapimage.BeginInit();
//        bitmapimage.StreamSource = memory;
//        bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
//        bitmapimage.EndInit();
//        DIIMG_For_Folder_ForListView = bitmapimage;
//        DicomImageModel diimgmodel = new DicomImageModel();
//        diimgmodel.diimgModel = bitmapimage;

//        //DicomImageModel.diimgModel = bitmapimage;

//        Alldicomimagediff.Add(diimgmodel);
//        //AllDicomImageList.Add(DIIMG_For_Folder);
//        //return bitmapimage;
//    }
//}



//    //for(int i = 0; i < 4; i++)
//    //{
//    //    for(int j = 0; j < qr4code; j++)
//    //    {

//    //    }
//    //}
//    //Hashtable hints = new Hashtable();
//    //hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
//    // Encode function is having Japanese characters as input 
//    //  ByteMatrix byteIMG = qrcodeWritter.Encode("親愛なる歓迎$#@25", BarcodeFormat.QR_CODE, 120, 120, hints);

//    qrcodeWritter.Write(ss)
//        .Save(@"C:\Demo_Project_Barcode\QRImage\" + $"{txtFileName.Text}.png");


//    //qrcodeWritter.Write("{ 'Name':" + $" '{txtName.Text}',\n " + "'Email:'" + $" ' {txtEmail.Text}', \n" + "'Name as the file to save':" + $" '{txtFileName.Text}'"+"}")
//    //    .Save(@"C:\Demo_Project_Barcode\QRImage\" + $"{txtFileName.Text}.png");


//    //qrcodeWritter.Write("Name" + $" {txtName.Text} \n" + "Email" + $"{txtEmail.Text} \n" + "Name as the file to save" + $"{txtFileName.Text}")
//    //    .Save(@"C:\Demo_Project_Barcode\QRImage\" + $"{txtFileName.Text}.bmp");


//    //  Console.WriteLine("QR Code is Generated");
//}

//private void btnLastTest1_Click(object sender, RoutedEventArgs e)
//{
//    Dictionary<EncodeHintType, object> encodeHint =
//                  new Dictionary<EncodeHintType, object>();
//    encodeHint.Add(EncodeHintType.CHARACTER_SET, ENCORD_NAME);
//    encodeHint.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.L);

//    string message = "testsdffffffffffffffffffffffffffffffffffasfasdadddddddddddddddddddddddddddddddddsdfsdfsdfsdd";  //1st Method Tried This but got black view
//    byte[] qrdata = Encoding.ASCII.GetBytes(message);
//    var sizestring = System.Text.ASCIIEncoding.Unicode.GetByteCount(message);
//    var hints = new Dictionary<EncodeHintType, object>
//    {
//        [EncodeHintType.MARGIN] = 0
//    };

//    //byte[] partbuff = new byte[0x21c];
//    //Array.Copy(qrData, 0x21c * page, partbuff, 0, 0x21c);


//    var qr = new QRCodeWriter();
//    int page = 4;
//    int size = 300;

//    var matrix = qr.encode_sa((byte)(4 & 0x2e), (byte)3, calcParity(qrdata), message, BarcodeFormat.QR_CODE, size, size, encodeHint);

//    var writer = new BarcodeWriter { Options = { Margin = 0 } };
//    Bitmap bitmap;
//    BitmapImage bitmapimage;
//    using (bitmap = writer.Write(matrix))
//    {
//        bitmap.MakeTransparent(System.Drawing.Color.White);

//        var ms = new MemoryStream();
//        bitmap.Save(ms, ImageFormat.Png);
//        bitmapimage = new BitmapImage();
//        bitmapimage.BeginInit();
//        bitmapimage.StreamSource = ms;
//        bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
//        bitmapimage.EndInit();
//        //return ms.ToArray();
//    }
//    // BitmapImage bitmapimage;
//    Bitmap bmp = bitmap;
//    //using (MemoryStream memory = new MemoryStream())
//    //{
//    //    bmp.Save(memory, ImageFormat.Png);

//    //    memory.Position = 0;
//    //    bitmapimage = new BitmapImage();
//    //    bitmapimage.BeginInit();
//    //    bitmapimage.StreamSource = memory;
//    //    bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
//    //    bitmapimage.EndInit();



//    //}
//    imgQR1.Source = bitmapimage;
//    string filePath = @"C:\Demo_Project_Barcode\QRImage\New_Image_Zxing\test50.png";
//    BitmapEncoder encoder = new PngBitmapEncoder();
//    encoder.Frames.Add(BitmapFrame.Create(bitmapimage));

//    using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
//    {
//        encoder.Save(fileStream);
//    }

//}



//private void btnLastTest2_Click1(object sender, RoutedEventArgs e)
//{
//    Dictionary<EncodeHintType, object> encodeHint =
//                 new Dictionary<EncodeHintType, object>();
//    encodeHint.Add(EncodeHintType.CHARACTER_SET, ENCORD_NAME);
//    encodeHint.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.L);

//    string message = "testsdffffffffffffffffffffffffffffffffffasfasdadddddddddddddddddddddddddddddddddsdfsdfsdfsdd";  //1st Method Tried This but got black view
//    byte[] qrdata = Encoding.ASCII.GetBytes(message);
//    int toBase = 16;
//    var sizestring = System.Text.ASCIIEncoding.Unicode.GetByteCount(message);
//    var sizest_hexa = Convert.ToString(sizestring, toBase);
//    var hints = new Dictionary<EncodeHintType, object>
//    {
//        [EncodeHintType.MARGIN] = 0
//    };

//    //byte[] partbuff = new byte[0x21c];
//    //Array.Copy(qrData, 0x21c * page, partbuff, 0, 0x21c);

//    var dividesize = sizestring / 4;

//    string hex = Convert.ToString(dividesize, toBase);
//    int page = 4;
//    int max = 2321322;
//    byte[] partbuff = new byte[0xb8];
//    //lock (partbuff ) Array.Copy(qrdata, 0x2e * page, partbuff, 0, 0xb8);
//    //lock (partbuff) Array.Copy(qrdata, partbuff, 0xb8);
//    //unsortedArray.CopyTo(unsortedArray2, 0);


//    partbuff = qrdata.ToArray();  //copy QRData into Partbuff Array     


//    string contents;

//    // contents = new String(partbuff, ENCORD_NAME);
//    // contents = new string(partbuff.ToString());
//    //contents=

//    var qr = new QRCodeWriter();

//    int size = 300;

//    var matrix = qr.encode_sa((byte)(1 & 0x2e), (byte)3, calcParity(qrdata), message, BarcodeFormat.QR_CODE, size, size, encodeHint);

//    var writer = new BarcodeWriter { Options = { Margin = 0 } };
//    Bitmap bitmap;
//    BitmapImage bitmapimage;
//    using (bitmap = writer.Write(matrix))
//    {
//        bitmap.MakeTransparent(System.Drawing.Color.White);

//        var ms = new MemoryStream();
//        bitmap.Save(ms, ImageFormat.Png);
//        bitmapimage = new BitmapImage();
//        bitmapimage.BeginInit();
//        bitmapimage.StreamSource = ms;
//        bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
//        bitmapimage.EndInit();
//        //return ms.ToArray();
//    }
//    // BitmapImage bitmapimage;
//    Bitmap bmp = bitmap;

//    imgQR1.Source = bitmapimage;
//    string filePath = @"C:\Demo_Project_Barcode\QRImage\New_Image_Zxing\test2.png";
//    BitmapEncoder encoder = new PngBitmapEncoder();
//    encoder.Frames.Add(BitmapFrame.Create(bitmapimage));

//    using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
//    {
//        encoder.Save(fileStream);
//    }

//}






//private void btnLastTest_Click(object sender, RoutedEventArgs e)
//{
//    ss = "testsdffffffffffffffffffffffffffffffffffasfasdadddddddddddddddddddddddddddddddddsdfsdfsdfsd";

//    byte[] qrdata = Encoding.ASCII.GetBytes(ss);
//    byte parity = calcParity(qrdata);
//    int size = 100; //This is width and height
//                    // string str = Encoding.ASCII.GetString(bytes);
//    int page = 400;
//    Bitmap bmp = createBinQRCode1(qrdata, size, page);
//    BitmapImage bmpimage;
//    BitmapImage bitmapimage;


//    using (MemoryStream memory = new MemoryStream())
//    {
//        bmp.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
//        memory.Position = 0;
//        bitmapimage = new BitmapImage();
//        bitmapimage.BeginInit();
//        bitmapimage.StreamSource = memory;
//        bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
//        bitmapimage.EndInit();



//    }
//    imgQR1.Source = bitmapimage;
//    string filePath = @"C:\Demo_Project_Barcode\QRImage\New_Image_Zxing\test.png";
//    BitmapEncoder encoder = new PngBitmapEncoder();
//    encoder.Frames.Add(BitmapFrame.Create(bitmapimage));

//    using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
//    {
//        encoder.Save(fileStream);
//    }


//    //bmpimage= (BitmapImage)Imaging.CreateBitmapSourceFromHBitmap(
//    //         hBitmap,
//    //         IntPtr.Zero,
//    //         Int32Rect.Empty,
//    //         BitmapSizeOptions.FromEmptyOptions());
//    //imgQR1.Source = bpm;
//    //using (MemoryStream memory = new MemoryStream())
//    //{
//    //    bitImgs.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
//    //    BitmapImage bitmapimage = new BitmapImage();
//    //    bitmapimage.BeginInit();
//    //    bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
//    //    bitmapimage.EndInit();
//    //}


//}







