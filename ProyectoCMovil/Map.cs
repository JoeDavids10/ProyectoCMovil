using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Support.V4.App;

namespace ProyectoCMovil
{
    [Activity(Label = "Map")]
    public class Map : Android.App.Activity,IOnMapReadyCallback
    {
        static readonly LatLng[] LocationForCustomIconMarkers =
        {
            new LatLng(4.619283, -74.137084)
        };

        GoogleMap googleMap;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.mapsLayout);
            MapFragment mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap map)
        {
            googleMap = map;
            googleMap.MyLocationEnabled = false;

            AddIconMarkerToMap();
            

            // Animate the move on the map so that it is showing the markers we added above.
            //googleMap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(LocationForCustomIconMarkers[1], 2));

            // Setup a handler for when the user clicks on a marker.
            googleMap.MarkerClick += MapOnMarkerClick;



            MarkerOptions markerOptions1 = new MarkerOptions();
            markerOptions1.SetPosition(new LatLng(4.619283, -74.137084));
            markerOptions1.SetTitle("CINE COLOMBIA LAS AMERICAS - OFERTA DISPONIBLE!");


            googleMap.AddMarker(markerOptions1);

            AddIconMarkerToMap();


            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.MoveCamera(CameraUpdateFactory.ZoomIn());

        }



        protected override void OnPause()
        {
            // Pause the GPS - we won't have to worry about showing the 
            // location.
            googleMap.MyLocationEnabled = false;

            googleMap.MarkerClick -= MapOnMarkerClick;
            //googleMap.InfoWindowClick -= HandleInfoWindowClick;

            base.OnPause();
        }


        void AddIconMarkerToMap()
        {
            for (var i = 0; i < LocationForCustomIconMarkers.Length; i++)
            {
                var icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.logoResize3);
                var markerOptions = new MarkerOptions()
                                    .SetPosition(LocationForCustomIconMarkers[i])
                                    .SetIcon(icon)
                                    .SetSnippet($"This is marker #{i}.")
                                    .SetTitle($"Marker {i}");
                googleMap.AddMarker(markerOptions);
            }
        }



        void MapOnMarkerClick(object sender, GoogleMap.MarkerClickEventArgs markerClickEventArgs)
        {
            markerClickEventArgs.Handled = true;

            var marker = markerClickEventArgs.Marker;
            if (marker.Id.Equals(marker))
            {
                StartActivity(typeof(GameActivity)); 
            }
            else
            {
                StartActivity(typeof(GameActivity));
                Toast.MakeText(this, $"INICIANDO JUEGO", ToastLength.Short).Show();
            }
        }



    }
}
