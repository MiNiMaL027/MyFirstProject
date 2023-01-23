﻿namespace List_Service.Services.ValidServices
{
    public static class ValidOptions // тут має бути валідатор, не статичний, який ти інжектиш в сервіс Validator
    {
        /// <summary>
        ///  Will check if the name meets the rules(<=3 and >=20)
        /// </summary>
        /// <param name="name"></param>
        /// <returns>True or False</returns>
        public static bool ValidName(string name) // 
        {  
            if (name.Length <= 3)
                return false;
            if(name.Length >= 20) 
                return false;
            return true;
        }
        /// <summary>
        /// Will check if the importance meets the rules(>3 and <1) 
        /// </summary>
        /// <param name="importanceValue"></param>
        /// <returns>True or False</returns>
        public static bool ValidImportance(int? importanceValue) // переплутав знаки більше-менше x>1 and x<3 
        {
            if (importanceValue == null)
                return true;
            if(importanceValue > 3)
                return false;
            if(importanceValue < 1)
                return false;          
            return true;
        }
    }
}
