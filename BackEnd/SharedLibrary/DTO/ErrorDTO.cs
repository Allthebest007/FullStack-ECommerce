using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DTO
{
    public class ErrorDTO
    {
        public List<string> Errors { get; private set; }

        // This property specify whether " IsShow " can be shown to client  
        public bool IsShow { get; private set; }

        public ErrorDTO()
        {
            Errors = new List<string>();
        }

        public ErrorDTO(string error, bool isShow) 
        {
            Errors = new List<string>();
            Errors.Add(error);
            IsShow = isShow;
        }

        public ErrorDTO(List<string> errors,bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }

    }
}
