using System;

namespace vindiniumcore.Infrastructure
{
    public class VindiniumSettings
    {
        public string Key => "";
        public bool TrainingMode => true;
        public int Turns => 400;
        public Uri ServerUrl => new Uri("http://vindinium.org/");
        public string Map => "m4";
    }
}