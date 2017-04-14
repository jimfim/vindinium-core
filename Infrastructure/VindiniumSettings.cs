using System;

namespace vindiniumcore.Infrastructure
{
    public class VindiniumSettings
    {
        public string Key => "4bxz95pv";
        public bool TrainingMode => true;
        public int Turns => 100;
        public Uri ServerUrl => new Uri("http://vindinium.org/");
        public string Map => "m4";
    }
}