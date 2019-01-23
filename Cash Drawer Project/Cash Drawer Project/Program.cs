using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cash_Drawer_Project
{
    class Program
    {
        static Dictionary<string, int> money_drawer = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            Decimal [] ChangeDueAndCashPaid = new Decimal [2];
            string[] AmountPaidCreditAndCardTypeAndAmountDue = new string[3];


                
            decimal FixedCostAmount = 0;
            decimal costAmount = 0;
            bool keepLooping = true;
            bool keepPurchasing = true;
            string makePurchases = "";
            string[] LogInfoArray = new string [7];
           


            InitializeTill(100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100);
           // InitializeTill(0, 0, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 10000000);
            
            while(keepPurchasing)
            
                {
                Console.WriteLine("Would you like to make a set of purchases? y/n");
                makePurchases = Console.ReadLine().ToLower();
                if(makePurchases == "y")
                {
                  FixedCostAmount = PurchaseCostFunction();
                   costAmount = FixedCostAmount;
                    keepPurchasing = true;
                    keepLooping = true;
                }else if (makePurchases == "n")
                {
                    keepPurchasing = false; 
                    break;
                }else
                {
                    Console.WriteLine("That is not valid input please enter:  'y/n'");
                    keepPurchasing = true;
                }                              


                while(keepLooping == true)
                {
                     Console.Write("\n\nSelect payment choice: 1.Cash  2.Card\t\t");
                     int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                case 1: 
                     ChangeDueAndCashPaid = CashPaymentFunction( costAmount);
                        if (ChangeDueAndCashPaid[0] > 0)
                        {
                            ReturnChangeFunction(ChangeDueAndCashPaid[0]);
                                keepLooping = false;
                        }else
                        {
                         keepLooping = false;
                        }
                     break;
          

                case 2:
                                                
                         AmountPaidCreditAndCardTypeAndAmountDue =  CardPaymentFunction(costAmount);
                            
                          if(Decimal.Parse(AmountPaidCreditAndCardTypeAndAmountDue[2]) == 0)
                          {
                               keepLooping = false;
                                costAmount = Decimal.Parse(AmountPaidCreditAndCardTypeAndAmountDue[2]);
                               
                          }else if(Decimal.Parse(AmountPaidCreditAndCardTypeAndAmountDue[2]) > 0)
                                {
                                    keepLooping = true;
                                    costAmount = Decimal.Parse(AmountPaidCreditAndCardTypeAndAmountDue[2]);

                                }                                
                      
                      break;
                default:
                        keepLooping = true;
                        break;
                }
                }
                LogInfoArray =   PopulateTransactionLogArray(AmountPaidCreditAndCardTypeAndAmountDue, ChangeDueAndCashPaid);
                keepPurchasing = true;
                Console.WriteLine("\n\n\n");

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "C:\\Users\\CCA009\\Documents\\Q Coding Accademy\\2018\\10-23-2018 Cash Drawer Transaction Log\\CashDrawer Transaction Log Project\\CashDrawer Transaction Log Project\\bin\\Debug\\CashDrawer Transaction Log Project.exe";
                startInfo.Arguments = LogInfoArray[0] + "  " + LogInfoArray[1] + "  " + LogInfoArray[2] + "  " + LogInfoArray[3] + "  " + LogInfoArray[4] + "  " + LogInfoArray[5] + "  " + LogInfoArray[6] ;
                Process.Start(startInfo);

             
   
                
            }
        }                         

        static string[] PopulateTransactionLogArray (string[] TempCardVendorAndCardPay, decimal[] tempChangeGivenAndCashPaid)
        {

            int TempTransNumber = 0;
            string TempTransTime;
            string date = "";
            TempTransNumber++;

            string [] tempArray = new string [7];

            switch (DateTime.Today.Month)
            {
                
                    case 1: {date = "Jan"; break;}
                    case 2: {date = "Feb"; break;}
                    case 3: {date = "Mar"; break;}
                    case 4: {date = "Apr"; break;}
                    case 5: {date = "May"; break;}
                    case 6: {date = "Jun"; break;}
                    case 7: {date = "Jul"; break;}
                    case 8: {date = "Aug"; break;}
                    case 9: {date = "Sep"; break;}
                    case 10: {date = "Oct"; break;}
                    case 11: {date = "Nov"; break;}
                    case 12: {date = "Dec"; break;}
                    default:
                    break;
               
            }
            TempTransNumber = TempTransNumber + 1;
            tempArray[0] = TempTransNumber.ToString();

            date += DateTime.Today.ToString("-dd-yyyy");
            tempArray[1]  = date ;     
            
            TempTransTime = DateTime.Now.ToString("hh:mm:sstt");
            tempArray[2] = TempTransTime ;

            string TempPaymentCash =  tempChangeGivenAndCashPaid[1].ToString();
            tempArray[3] = TempPaymentCash ;

            string TempCardVendor = TempCardVendorAndCardPay [1];
            tempArray[4] = TempCardVendor;           
            if(tempArray[4] == null)
            {
                tempArray[4] = "No Card Type";
            }

            string TempAmountPaidOnCard = TempCardVendorAndCardPay[0];
            tempArray[5] = TempAmountPaidOnCard;
            if(tempArray[5] == null)
            {
               tempArray[5]  = "No Card Payment";
            }

            string TempChangeGiven = tempChangeGivenAndCashPaid[0].ToString();
            tempArray[6] = TempChangeGiven;

            return  tempArray  ;
        }
        
        //this function will populate the dictionary in order to dictate how much money is available in your cash register that day.
        static bool InitializeTill(int Hundreds, int Fifties, int Twenties, int Tens, int Fives, int Twos, int OnesB, int OnesCoin, int FiftyCent, int Quarters, int Dimes, int Nickels, int Pennies)
        {     
            money_drawer.Add("Hundreds", Hundreds);
            money_drawer.Add("Fifties", Fifties);
            money_drawer.Add("Twenties", Twenties);
            money_drawer.Add("Ten", Tens);
            money_drawer.Add("Five", Fives);
            money_drawer.Add("Two", Twos);
            money_drawer.Add("One Bill", OnesB);
            money_drawer.Add("One Coin", OnesCoin);
            money_drawer.Add("FiftyCent", FiftyCent);
            money_drawer.Add("Quarters", Quarters);
            money_drawer.Add("Dimes", Dimes);
            money_drawer.Add("Nickles", Nickels);
            money_drawer.Add("Pennies", Pennies );
            return true;
        }
        
        static decimal PurchaseCostFunction()
        {
            bool keepLooping = true;
            int itemCount = 1;
            decimal itemAmount = 0;
            decimal itemTotal =0;
            string userInput = "";
            Console.WriteLine("List your purchases below\n\n" );
            while (keepLooping)
            {
                Console.Write("Item {0}:   $" ,itemCount );
                userInput = Console.ReadLine();
                
                if (userInput == " " || userInput == "")
                {
                    Console.WriteLine("");
                    keepLooping = false;
                }
                else 
                {
                    itemAmount = decimal.Parse(userInput);
                    itemTotal = itemTotal + itemAmount;
              
                }
                itemCount++;                
            }
            Console.WriteLine("\nThe total amount of your purchases is: {0}", itemTotal);
            return itemTotal;
            
        }

        static decimal[] CashPaymentFunction( decimal AmountDue)
        {
            decimal [] ChangeDueAndTotalCashPaid = new decimal [2];
            int counter = 1;
            bool keepLooping = true;
            decimal changeOwed = 0;
            decimal billOrCoinAmount = 0;
            decimal totalCashPaid = 0;

            Console.WriteLine("Accepting cash payment");

            Console.WriteLine( "\n\nThe total amount due is:  {0}", AmountDue);
            Console.WriteLine("\nEnter payments below: \n\n");
            while (keepLooping)
            {
                Console.Write("Payment {0}:  ", counter);
                billOrCoinAmount = decimal.Parse(Console.ReadLine());
                if(billOrCoinAmount > AmountDue)
                {
                    changeOwed = billOrCoinAmount - AmountDue;
                    totalCashPaid = totalCashPaid + billOrCoinAmount;
                    break;
                }else if(billOrCoinAmount == AmountDue)
                {

                    totalCashPaid = totalCashPaid + billOrCoinAmount;
                    Console.WriteLine("Remaining   0.00\n");
                    Console.WriteLine("Thanks for shopping. Come Again!");
                    Console.ReadKey();
                    break;//FIX THIS __ THERE IS A LOOP HERE THAT TAKES YOU BACK TO THE SWITCH
                }else if(billOrCoinAmount < AmountDue && billOrCoinAmount == 100)
                {
                    money_drawer["Hundreds"] ++;
                    AmountDue = AmountDue - 100;
                    counter ++;
                    totalCashPaid = totalCashPaid + billOrCoinAmount;
                    Console.WriteLine("Remaining   {0}\n",AmountDue);
                    
                }else if(billOrCoinAmount < AmountDue && billOrCoinAmount == 50)
                {
                    money_drawer["Fifties"] ++;
                    AmountDue = AmountDue - 50;
                    counter ++;
                    totalCashPaid = totalCashPaid + billOrCoinAmount;
                    Console.WriteLine("Remaining   {0}\n",AmountDue);
                    
                }else if(billOrCoinAmount < AmountDue && billOrCoinAmount == 20)
                {
                    money_drawer["Twenties"] ++;
                    AmountDue = AmountDue - 20;
                    counter ++;
                     totalCashPaid = totalCashPaid + billOrCoinAmount;
                    Console.WriteLine("Remaining   {0}\n",AmountDue);
                    
                }else if(billOrCoinAmount < AmountDue && billOrCoinAmount == 10)
                {
                    money_drawer["Ten"] ++;
                    AmountDue = AmountDue - 10;
                    counter ++;
                     totalCashPaid = totalCashPaid + billOrCoinAmount;
                    Console.WriteLine("Remaining   {0}\n",AmountDue);
                    
                }else if(billOrCoinAmount < AmountDue && billOrCoinAmount == 5)
                {
                    money_drawer["Five"] ++;
                    AmountDue = AmountDue - 5;
                    counter ++;
                    totalCashPaid = totalCashPaid + billOrCoinAmount;
                    Console.WriteLine("Remaining   {0}\n",AmountDue);
                    
                }else if(billOrCoinAmount < AmountDue && billOrCoinAmount == 2)
                {
                    money_drawer["Two"] ++;
                    AmountDue = AmountDue - 2;
                    counter ++;
                    totalCashPaid = totalCashPaid + billOrCoinAmount;
                    Console.WriteLine("Remaining   {0}\n",AmountDue);
                    
                }else if(billOrCoinAmount < AmountDue && billOrCoinAmount == 1)
                {
                    money_drawer["One Bill"] ++;
                    AmountDue = AmountDue - 1;
                    counter ++;
                    totalCashPaid = totalCashPaid + billOrCoinAmount;
                    Console.WriteLine("Remaining   {0}\n",AmountDue);
                    
                }else if(billOrCoinAmount < AmountDue && billOrCoinAmount == 1)
                {
                    money_drawer["One Coin"] ++;
                    AmountDue = AmountDue - 1;
                    counter ++;
                    totalCashPaid = totalCashPaid + billOrCoinAmount;
                    Console.WriteLine("Remaining   {0}\n",AmountDue);
                    
                }else if(billOrCoinAmount < AmountDue && billOrCoinAmount == .50m)
                {
                    money_drawer["FiftyCent"] ++;
                    AmountDue = AmountDue - .50m;
                    counter ++;
                    totalCashPaid = totalCashPaid + billOrCoinAmount;
                    Console.WriteLine("Remaining   {0}\n",AmountDue);
                    
                }else if(billOrCoinAmount < AmountDue && billOrCoinAmount == .25m)
                {
                    money_drawer["Quarters"] ++;
                    AmountDue = AmountDue - .25m;
                    counter ++;
                     totalCashPaid = totalCashPaid + billOrCoinAmount;
                    Console.WriteLine("Remaining   {0}\n",AmountDue);
                    
                }else if(billOrCoinAmount < AmountDue && billOrCoinAmount == .10m)
                {
                    money_drawer["Dimes"] ++;
                    AmountDue = AmountDue - .10m;
                    counter ++;
                    totalCashPaid = totalCashPaid + billOrCoinAmount;
                    Console.WriteLine("Remaining   {0}\n",AmountDue);
                    
                }else if(billOrCoinAmount < AmountDue && billOrCoinAmount == .05m)
                {
                    money_drawer["Nickles"] ++;
                    AmountDue = AmountDue - .05m;
                    counter ++;
                    totalCashPaid = totalCashPaid + billOrCoinAmount;
                    Console.WriteLine("Remaining   {0}\n",AmountDue);
                    
                }else if(billOrCoinAmount < AmountDue && billOrCoinAmount == .01m)
                {
                    money_drawer["Pennies"] ++;
                    AmountDue = AmountDue - .01m;
                    counter ++;
                    totalCashPaid = totalCashPaid + billOrCoinAmount;
                    Console.WriteLine("Remaining   {0}\n",AmountDue);
                    
                }
            }
            ChangeDueAndTotalCashPaid[0] = changeOwed;
            ChangeDueAndTotalCashPaid[1] = totalCashPaid;

            Console.WriteLine("Your change will be:   {0}", changeOwed);
            
            return ChangeDueAndTotalCashPaid;
        }

        static void ReturnChangeFunction( decimal changeDue)
        {
            int [] counterArray = new int[13];
            bool keepLooping = true;
            bool keepLoopingPrintChange = true;
            bool notEnoughChange = false;
            
           
            while (keepLooping)
            {
                 if(changeDue >= 100 && money_drawer["Hundreds"] > 0)
                 {
                    changeDue = changeDue - 100;
                    money_drawer["Hundreds"] --;
                    counterArray[0]++;
                 }else if (changeDue >= 50 && money_drawer["Fifties"] > 0)
                {
                    changeDue = changeDue - 50;
                    money_drawer["Fifties"]--;      
                    counterArray[1]++;
                }else if (changeDue >= 20 && money_drawer["Twenties"] > 0)
                {
                    changeDue = changeDue - 20;
                    money_drawer["Twenties"]--;
                    counterArray[2]++;
                }else if (changeDue >= 10 && money_drawer["Ten"] > 0)
                {
                    changeDue = changeDue - 10;
                    money_drawer["Ten"]--;
                   counterArray[3]++;
                }else if (changeDue >= 5 && money_drawer["Five"] > 0)
                {
                    changeDue = changeDue - 5;
                    money_drawer["Five"]--;
                   counterArray[4]  ++;
                }else if (changeDue >= 2 && money_drawer["Two"] > 0)
                {
                    changeDue = changeDue - 2;
                    money_drawer["Two"]--;
                  counterArray[5] ++;
                }else if (changeDue >= 1 && money_drawer["One Bill"] > 0)
                {
                    changeDue = changeDue - 1;
                    money_drawer["One Bill"]--;
                  counterArray[6] ++;
                }else if (changeDue >= 1 && money_drawer["One Coin"] > 0)
                {
                    changeDue = changeDue - 1;
                    money_drawer["One Coin"]--;
                  counterArray[7] ++;
                }else if (changeDue >= .50m && money_drawer["FiftyCent"] > 0)
                {
                    changeDue = changeDue - .50m;
                    money_drawer["FiftyCent"]--;
                   counterArray[8] ++;
                }else if (changeDue >= .25m && money_drawer["Quarters"] > 0)
                {
                    changeDue = changeDue - .25m;
                    money_drawer["Quarters"]--;
                  counterArray[9] ++;
                }else if (changeDue >= .10m && money_drawer["Dimes"] > 0)
                {
                    changeDue = changeDue - .10m;
                    money_drawer["Dimes"]--;
                   counterArray[10] ++;
                }else if (changeDue >= .05m && money_drawer["Nickles"] > 0)
                {
                    changeDue = changeDue - .05m;
                    money_drawer["Nickles"]--;
                    counterArray[11] ++;
                }else if (changeDue >= .01m && money_drawer["Pennies"] > 0)
                {
                    changeDue = changeDue - .01m;
                    money_drawer["Pennies"]--;
                   counterArray[12]++;
                } else if( changeDue == 0)
                {
                     
                    keepLooping = false;
                }
                else
                {
                    Console.WriteLine("Not enough change in this machine to process your cash payment, please use another method of payment.");
                    keepLooping = false;
                        
                        notEnoughChange = true;
                }
            }
            Console.WriteLine("\n\n");
           while(keepLoopingPrintChange){
                if(counterArray[0] > 0 && notEnoughChange == false)
                {
                    Console.WriteLine("You get {0} hundred dollar bill(s) back.",counterArray[0]);
                    counterArray[0] = 0;
                }else if(counterArray[1] > 0 && notEnoughChange == false)
                {
                    Console.WriteLine("You get {0} fifty dollar bill(s) back", counterArray[1]);
                    counterArray[1] = 0;
                }else if(counterArray[2] > 0 && notEnoughChange == false)
                {
                    Console.WriteLine("You get {0} twenty dollar bill(s) back", counterArray[2]);
                    counterArray[2] = 0;
                }else if(counterArray[3] > 0 && notEnoughChange == false)
                {
                    Console.WriteLine("You get {0} ten dollar bill(s) back", counterArray[3]);
                    counterArray[3] = 0;
                }else if(counterArray[4] > 0 && notEnoughChange == false)
                {
                    Console.WriteLine("You get {0} five dollar bill(s) back", counterArray[4]);
                    counterArray[4] = 0;
                }else if(counterArray[5] > 0 && notEnoughChange == false)
                {
                    Console.WriteLine("You get {0} two dollar bill(s) back", counterArray[5]);
                    counterArray[5] = 0;
                }else if(counterArray[6] > 0 && notEnoughChange == false)
                {
                    Console.WriteLine("You get {0} one dollar bill(s) back", counterArray[6]);
                    counterArray[6] = 0;
                }else if(counterArray[7] > 0 && notEnoughChange == false)
                {
                    Console.WriteLine("You get {0} one dollar coin(s) back", counterArray[7]);
                    counterArray[7] = 0;
                }else if(counterArray[8] > 0 && notEnoughChange == false)
                {
                    Console.WriteLine("You get {0} fifty cent coin(s) back", counterArray[8]);
                    counterArray[8] = 0;
                }else if(counterArray[9] > 0 && notEnoughChange == false)
                {
                    Console.WriteLine("You get {0} quarter(s) back", counterArray[9]);
                    counterArray[9] = 0;
                }else if(counterArray[10] > 0 && notEnoughChange == false)
                {
                    Console.WriteLine("You get {0} dime(s) back", counterArray[10]);
                    counterArray[10] = 0;
                }else if(counterArray[11] > 0 && notEnoughChange == false)
                {
                    Console.WriteLine("You get {0} nickle(s) back", counterArray[11]);
                    counterArray[11] = 0;
                }else if(counterArray[12] > 0 && notEnoughChange == false)
                {
                    Console.WriteLine("You get {0} pennie(s) back", counterArray[12]);
                    counterArray[12] = 0;
                }else if(counterArray[0] == 0 && counterArray[1] ==0 && counterArray[2]==0&& counterArray[3]==0 && counterArray[4]==0 && counterArray[5]==0 && counterArray[6]==0&& counterArray[7]==0 && counterArray[8] == 0 && counterArray[9]== 0 && counterArray[10]== 0 && counterArray[11]== 0 && counterArray[12] == 0)
                {
                    keepLoopingPrintChange = false;
                }
            }
            
            
            Console.WriteLine("\n\nThanks for shopping! Good-Bye!!");
           

        }

        static decimal CashBackFunction(decimal AmountDue)
        {

            string cashBackChoice = "";
            decimal cashBackAmount = 0;
            decimal costAmount = 0;

            Console.WriteLine("Would you like to recieve cash back?  y/n");
            cashBackChoice = Console.ReadLine().ToLower();
            
            if(cashBackChoice == "y")
            {
                  Console.WriteLine("How much cash back would you like to receieve?");
                  cashBackAmount =decimal.Parse(Console.ReadLine());
                  costAmount = AmountDue + cashBackAmount;
                Console.WriteLine("The total amount charged to your card will be: {0}",costAmount);
            }else if(cashBackChoice == "n")
            {
                costAmount = AmountDue;
            }
            return costAmount;
        }

        static string[] CardPaymentFunction(decimal AmountDue)
        {
            string[] tempAmountPaidCreditAndCardTypeAndAmountDue = new string[3];
            string creditCardNumber = "";
            string cardType = "";
            decimal amountApproved = 0;
            string[] MoneyRequestInfo = new string[2];
            decimal costAmount = 0;
   
        
            costAmount = CashBackFunction(AmountDue);
            AmountDue = costAmount; 

            Console.WriteLine("Please enter your credit card number below:");
            creditCardNumber = Console.ReadLine(); 

            if (PassesLuhnTest(creditCardNumber) == true)
            {
                cardType = CheckCardType(creditCardNumber);
                MoneyRequestInfo = MoneyRequest(creditCardNumber, AmountDue);
                if(MoneyRequestInfo[1] != "declined")
                {
                    amountApproved = decimal.Parse(MoneyRequestInfo[1]);
                }
                if(MoneyRequestInfo[1]== "declined")
                {
                    Console.WriteLine("Your {0} has been declined.",cardType);
                    Console.WriteLine("\nYou still owe {0} for your purchases", AmountDue);
                }else if (amountApproved == AmountDue)
                {
                    Console.WriteLine("You have paid {0} with your {1} card!",AmountDue, cardType);
                    AmountDue = 0;
                }else if(amountApproved < AmountDue)
                {
                    AmountDue = AmountDue - amountApproved;
                    Console.WriteLine("You have paid {0} with your {1} card. You still owe {2} for your purchases.",amountApproved, cardType, AmountDue);

                //if program reaches this point we need to loop
                }               
            }
            else
            {
                Console.WriteLine("This is not a valid card number. Please enter card number again or use alternate payment method.");
               
            }

            tempAmountPaidCreditAndCardTypeAndAmountDue[0] = amountApproved.ToString();
            tempAmountPaidCreditAndCardTypeAndAmountDue[1] = cardType.ToString();
            tempAmountPaidCreditAndCardTypeAndAmountDue[2] = AmountDue.ToString();
             return tempAmountPaidCreditAndCardTypeAndAmountDue;
            
        }

        public static bool PassesLuhnTest(string cardNumber)
        {
            //Clean the card number- remove dashes and spaces
            cardNumber = cardNumber.Replace("-", "").Replace(" ", "");
    
            //Convert card number into digits array
            int[] digits = new int[cardNumber.Length];
            for (int i = 0; i < cardNumber.Length; i++)
            {
                 digits[i] = int.Parse(cardNumber.Substring(i, 1));
            }


             int sum = 0;
             bool alt = false;
             for (int i = digits.Length - 1; i >= 0; i--)
             {
               int curDigit = digits[i];
                if (alt)
                {
                  curDigit *= 2;
                   if (curDigit > 9)
                   {
                     curDigit -= 9;
                   }
                }
                 sum += curDigit;
                 alt = !alt;
             }       

             //If Mod 10 equals 0, the number is good and this will return true
            return sum % 10 == 0;
        }

        static string CheckCardType(string cardNumber)
        {

            string cardType = "";
              //Clean the card number- remove dashes and spaces
             cardNumber = cardNumber.Replace("-", "").Replace(" ", "");


            //Convert card number into digits array
            int[] digits = new int[cardNumber.Length];
            for (int i = 0; i < cardNumber.Length; i++)
            {
                 digits[i] = int.Parse(cardNumber.Substring(i, 1));
            }

            if((digits[0]  == 3 && digits[1] == 4) ||( digits[0] == 3 && digits[1] == 7))
            {
                cardType = "American Express";
            } else if((digits[0]  == 4 ))
            {
                cardType = "Visa";
            } else if((digits[0]  == 5 && digits[1] == 1) ||( digits[0] == 5 && digits[1] == 2)||( digits[0] == 5 && digits[1] == 3)||( digits[0] == 5 && digits[1] == 4)||( digits[0] == 5 && digits[1] == 5))
            {
                cardType = "Master Card";
            }else if((digits[0]  == 6 && digits[1] == 0 && digits[2] == 1 && digits [3] == 1) ||( digits[0] == 6 && digits[1] == 4 && digits[2] == 4)||( digits[0] == 6 && digits[1] == 5))
            {
                cardType = "Discover";
            }

            return cardType;

        }

        static string [] MoneyRequest(string account_number, decimal amount)
        {
            Random rnd = new Random();
            //50% chance transaction passes or fails
            bool pass = rnd.Next(100) < 50;
            //50% chance that a failed transaction is declined
            bool declined = rnd.Next(100) < 50;

            if (pass)
            {
                return new string [] {account_number, amount.ToString() };
            }else{
            if (!declined)
            {
                return new string [] {account_number,  (amount / rnd.Next(2,6)).ToString()};
            }else
            {
                return new string[] {account_number, "declined"};
            }//end if
        }//end if
    }
    }
}
