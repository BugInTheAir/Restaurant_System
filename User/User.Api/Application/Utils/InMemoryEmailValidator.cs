using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Application.Utils
{
    public class InMemoryEmailValidator
    {
        private Dictionary<string, string> _listValidation;
        private static InMemoryEmailValidator inMemoryEmailValidator;
        private InMemoryEmailValidator()
        {
            _listValidation = new Dictionary<string, string>();
        }
        public void AddEmailValidationToList(string email, string codeToValidate)
        {
            if (!_listValidation.ContainsKey(email))
            {
                _listValidation.Add(email, codeToValidate);
            }
            else
            {
                _listValidation.Remove(email);
                _listValidation.Add(email, codeToValidate);
            }
        }
        public bool VerifyEmail(string email, string codeToValidate)
        {
            var result = _listValidation.Where(x => x.Key == email).FirstOrDefault();
            if (result.Value.Equals(codeToValidate))
            {
                _listValidation.Remove(email);
                return true;
            }
            return false;
        }
        public static InMemoryEmailValidator GetInstance()
        {
            return inMemoryEmailValidator ?? new InMemoryEmailValidator();
        }

    }
}
