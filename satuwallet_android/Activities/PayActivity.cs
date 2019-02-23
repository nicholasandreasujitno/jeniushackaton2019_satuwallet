using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Vision;
using Android.Gms.Vision.Barcodes;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using satuwallet_android.Constants;
using static Android.Gms.Vision.Detector;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;

namespace satuwallet_android.Activities
{
    [Activity(Label = "Pay", Theme = "@style/AppTheme")]
    public class PayActivity : AppCompatActivity, ISurfaceHolderCallback, IProcessor
    {
        SurfaceView surfaceView;
        TextView tvResult;
        BarcodeDetector barcodeDetector;
        CameraSource cameraSource;
        const int RequestCameraPermisionID = 1001;

        private V7Toolbar mToolbar;
        private RadioGroup mRdgPlatform;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_pay);

            mToolbar = FindViewById<V7Toolbar>(Resource.Id.pay_toolbar);
            SetSupportActionBar(mToolbar);

            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            // SURFACE BARCODE INIT
            surfaceView = FindViewById<SurfaceView>(Resource.Id.cameraView);
            tvResult = FindViewById<TextView>(Resource.Id.txtResult);
            //Bitmap bitMap = BitmapFactory.DecodeResource(ApplicationContext.Resources, Resource.Drawable.icons8_qr_code_24);
            barcodeDetector = new BarcodeDetector.Builder(this)
                //.SetBarcodeFormats(BarcodeFormat.Code93 | BarcodeFormat.Code128 | BarcodeFormat.Code39 | 
                //    BarcodeFormat.Ean8 | BarcodeFormat.Ean13 |
                //    BarcodeFormat.DataMatrix | 
                //    BarcodeFormat.QrCode)
                .Build();
            cameraSource = new CameraSource
                .Builder(this, barcodeDetector)
                .SetAutoFocusEnabled(true)
                .SetRequestedPreviewSize(640, 480)
                .Build();
            surfaceView.Holder.AddCallback(this);
            barcodeDetector.SetProcessor(this);

            mRdgPlatform = FindViewById<RadioGroup>(Resource.Id.pay_rdgPlatform);

            // Radio group options
            var isFirst = true;
            foreach (var p in Enum.GetValues(typeof(Platform)))
            {
                RadioButton rb = new RadioButton(this);
                //rb.SetTextColor(Android.Graphics.Color.Black);
                rb.Id = (int)p;
                rb.Text = p.ToString();
                if (isFirst)
                {
                    rb.Checked = true;
                    isFirst = false;
                }
                //rb.SetTextSize(ComplexUnitType.Dip, 25);
                mRdgPlatform.AddView(rb);
            }


            var btnMyQR = FindViewById<Button>(Resource.Id.pay_btnMyQR);
            btnMyQR.Click += BtnMyQR_Click;


        }

        private void BtnMyQR_Click(object sender, EventArgs e)
        {
            var i = new Intent(Application.Context, typeof(MyQRActivity));
            StartActivity(i);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
            }
            return base.OnOptionsItemSelected(item);
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestCameraPermisionID:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            if (ActivityCompat.CheckSelfPermission(ApplicationContext, Manifest.Permission.Camera) != Android.Content.PM.Permission.Granted)
                            {
                                //Request Permision  
                                ActivityCompat.RequestPermissions(this, new string[]
                                {
                                    Manifest.Permission.Camera
                                }, RequestCameraPermisionID);
                                return;
                            }
                            try
                            {
                                cameraSource.Start(surfaceView.Holder);
                            }
                            catch (InvalidOperationException)
                            {
                            }
                        }
                    }
                    break;
            }
        }

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            if (ActivityCompat.CheckSelfPermission(ApplicationContext, Manifest.Permission.Camera) != Android.Content.PM.Permission.Granted)
            {
                //Request Permision  
                ActivityCompat.RequestPermissions(this, new string[]
                {
                    Manifest.Permission.Camera
                }, RequestCameraPermisionID);
                return;
            }
            try
            {
                cameraSource.Start(surfaceView.Holder);
            }
            catch (InvalidOperationException)
            {
            }
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            cameraSource.Stop();
        }

        //private bool isCheckingQR = false;
        private bool isCheckingBarcode = false;
        private bool isValid = false;
        public void ReceiveDetections(Detections detections)
        {
            SparseArray qrcodes = detections.DetectedItems;
           
            if (qrcodes.Size() != 0)
            {
                tvResult.Post(() =>
                {
                    //Vibrator vibrator = (Vibrator)GetSystemService(Context.VibratorService);
                    //vibrator.Vibrate(1000);
                    tvResult.Text = ((Barcode)qrcodes.ValueAt(0)).RawValue;
                });
                if (!isCheckingBarcode)
                {
                    isCheckingBarcode = false;
                    // Do barcode checking here
                    isValid = true; // assign valid or not
                    isCheckingBarcode = true;

                    // Then redirect to other page
                    if (isValid)
                    {
                        var i = new Intent(Application.Context, typeof(PayResultActivity));
                        i.PutExtra("barcode", ((Barcode)qrcodes.ValueAt(0)).RawValue);
                        i.PutExtra("plaftormId", mRdgPlatform.CheckedRadioButtonId);
                        StartActivity(i);
                        Finish();
                    }
                }
            }
        }
        public void Release()
        {
        }
    }
}