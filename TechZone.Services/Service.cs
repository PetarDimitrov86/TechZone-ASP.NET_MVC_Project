﻿namespace TechZone.Services
{
    using Data;
    using System.IO;
    using System.Threading.Tasks;
    using Dropbox.Api;
    using Dropbox.Api.Files;

    public abstract class Service
    {
        protected Service()
        {
            this.Context = new TechZoneContext();
        }

        protected TechZoneContext Context { get; }

        protected async Task Upload(DropboxClient dbx, string folder, string file, byte[] content)
        {
            using (var mem = new MemoryStream(content))
            {
                var updated = await dbx.Files.UploadAsync(
                    folder + "/" + file,
                    WriteMode.Overwrite.Instance,
                    body: mem);
            }
        }
    }
}