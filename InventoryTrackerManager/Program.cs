using System.Diagnostics;
using System.Globalization;

string[] itemName = new string[10];
int[] itemQuantity = new int[itemName.Length];
int[] itemPrice = new int[itemName.Length];
int itemCount = 0;
bool isExit = false;

while (!isExit)
{
    Console.WriteLine("\nWelcome to the Inventory Management System");
    Console.WriteLine(
        " 1. Add Item\n 2. Update Item\n 3. Search\n 4. View all items\n 5. Generate Report\n 6. Exit"
    );
    int chooseOption = 0;
    bool isVaildOption = false;

    if (itemCount >= itemName.Length)
    {
        //Here ref keyword means that output is modifying input array that is itemName
        //So, there is no need of pointing it to again itemName
        Array.Resize(ref itemName, itemName.Length + 10);
        Array.Resize(ref itemQuantity, itemQuantity.Length + 10);
        Array.Resize(ref itemPrice, itemPrice.Length + 10);
    }

    while (!isVaildOption)
    {
        Console.Write("Enter a number: ");
        string? input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input) && int.TryParse(input, out chooseOption))
        {
            isVaildOption = true;
        }
        else
        {
            Console.WriteLine("Please enter a valid option");
            continue;
        }
    }

    switch (chooseOption)
    {
        case 1:
            addItem();
            break;

        case 2:
            updateItem();
            break;

        case 3:
            searchItem();
            break;

        case 4:
            viewAllItems();
            break;

        case 5:
            generateReport();
            break;

        case 6:
            isExit = true;
            break;
    }
}

void addItem()
{
    int quantity = 0;
    int price = 0;

    Console.Write("Enter the name of the item:");
    string name = Console.ReadLine().Trim().ToLower();

    Console.Write("Enter the quantity of the item:");
    string quantityInput = Console.ReadLine().Trim().ToLower();

    Console.Write("Enter the item price:");
    string priceInput = Console.ReadLine().Trim().ToLower();

    bool isVaild =
        !string.IsNullOrEmpty(name)
        && !string.IsNullOrEmpty(quantityInput)
        && !string.IsNullOrEmpty(priceInput)
        && int.TryParse(quantityInput, out quantity)
        && int.TryParse(priceInput, out price);

    if (!isVaild)
    {
        Console.WriteLine("Enter the valid name, quantity and price");
        return;
    }

    if (isVaild)
    {
            itemName[itemCount] = name;
            itemQuantity[itemCount] = quantity;
            itemPrice[itemCount] = price;
            itemCount++;
            Console.WriteLine("Item added successfully");
    }
}

void updateItem()
{
    int index = 0;
    int quantity = 0;
    int price = 0;

    viewAllItems();

    Console.WriteLine();
    Console.WriteLine("Enter the item id:");
    string input = Console.ReadLine().Trim().ToLower();
    ;

    bool isValid = !string.IsNullOrEmpty(input) && int.TryParse(input, out index);

    if (!isValid)
    {
        Console.WriteLine("Please enter the valid id:");
        return;
    }

    if (isValid)
    {
        for (int i = 0; i < itemCount; i++)
        {
            if (i == index - 1)
            {
                Console.Write(
                    " 1. Modify Name, quantity and price\n 2. modify quantity\n 3. Modify price "
                );
                string optionsInput = Console.ReadLine();

                if (
                    !string.IsNullOrEmpty(optionsInput)
                    && int.TryParse(optionsInput, out int option)
                )
                {
                    switch (option)
                    {
                        case 1:

                            Console.Write("Enter item name to update");
                            string name = Console.ReadLine().Trim().ToLower();
                            Console.Write("Enter item quantity to update");
                            string quantityInput = Console.ReadLine().Trim().ToLower();
                            Console.Write("Enter item price to update");
                            string priceInput = Console.ReadLine().Trim().ToLower();

                            bool isVaild =
                                !string.IsNullOrEmpty(name)
                                && !string.IsNullOrEmpty(quantityInput)
                                && !string.IsNullOrEmpty(priceInput)
                                && int.TryParse(quantityInput, out quantity)
                                && int.TryParse(priceInput, out price);

                            if (isVaild)
                            {
                                itemName[i] = name;
                                itemQuantity[i] = quantity;
                                itemPrice[i] = price;
                                Console.WriteLine("Successfully updated!");
                                break;
                            }
                            break;

                        case 2:

                            Console.Write("Enter item quantity to update");
                            string quantityUpdate = Console.ReadLine().Trim().ToLower();

                            bool isVaildQunatity =
                                !string.IsNullOrEmpty(quantityUpdate)
                                && int.TryParse(quantityUpdate, out quantity);

                            if (isVaildQunatity)
                            {
                                itemQuantity[i] = quantity;
                                Console.WriteLine("Successfully updated!");
                                break;
                            }
                            break;

                        case 3:

                            Console.Write("Enter item price to update");
                            string priceUpdate = Console.ReadLine().Trim().ToLower();

                            bool isVaildPrice =
                                !string.IsNullOrEmpty(priceUpdate)
                                && int.TryParse(priceUpdate, out price);

                            if (isVaildPrice)
                            {
                                itemPrice[i] = price;
                                Console.WriteLine("Successfully updated!");
                                break;
                            }
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Sorry select valid number, try again");
                }
            }
        }
    }
}

void searchItem()
{
    Console.WriteLine("Search by command (e.g., name:apple or price>1000):");
    string input = Console.ReadLine().Trim().ToLower();

    if (string.IsNullOrEmpty(input))
    {
        Console.WriteLine("Enter a valid input");
        return;
    }

    string key = "";
    string value = "";
    char operatorSymbol = ' ';

    if (input.Contains(":"))
    {
        operatorSymbol = ':';
    }
    else if (input.Contains(">"))
    {
        operatorSymbol = '>';
    }
    else if (input.Contains("<"))
    {
        operatorSymbol = '<';
    }
    else
    {
        Console.WriteLine("Invalid search format.");
        return;
    }

    int index = input.IndexOf(operatorSymbol);
    key = input.Substring(0, index).Trim();
    value = input.Substring(index + 1).Trim();

    bool itemFound = false;

    if (key == "name")
    {
        for (int i = 0; i < itemCount; i++)
        {
            if (itemName[i].Contains(value))
            {
                Console.WriteLine($"{itemName[i],-15} | {itemQuantity[i],10} | {itemPrice[i],-5:C}");
                itemFound = true;
            }
        }
    }
    else if ((key == "price" || key == "quantity") && int.TryParse(value, out int numericValue))
    {
        for (int i = 0; i < itemCount; i++)
        {
            int compareValue = key == "price" ? itemPrice[i] : itemQuantity[i];

            if ((operatorSymbol == '>' && compareValue > numericValue) ||
                (operatorSymbol == '<' && compareValue < numericValue))
            {
                Console.WriteLine($"{itemName[i],-15} | {itemQuantity[i],10} | {itemPrice[i],-5:C}");
                itemFound = true;
            }
        }
    }
    else
    {
        Console.WriteLine("Invalid key or numeric value.");
        return;
    }

    if (!itemFound)
    {
        Console.WriteLine("No matching items found.");
    }
}

void generateReport() {

    int totalQuantity = 0;
    decimal totalValue = 0;

    for (int i = 0; i< itemCount; i++) {
        totalQuantity += itemQuantity[i];
        totalValue+=itemQuantity[i] * itemPrice[i];
    }


    //SAYNTAX OF STRING.FORMAT
    //string.Format("{index,[alignment]:[format]}", value)
    //Can not add 2 alignments once

    Console.WriteLine();
    string header = "INVENTORY REPORT";
    int totalWidth = 50;
    int leftPadding = (totalWidth - header.Length)/2;
    int rightPadding = totalWidth - header.Length - leftPadding;
    string separator = new string('-', 52);

    string stringHeader = string.Format("{0} {1} {2}", new string('=',leftPadding), header, new string('=',rightPadding));

    Console.WriteLine(stringHeader) ;
    Console.WriteLine() ;

    string title = string.Format("{0,-15} | {1,10} | {2,-5:C}", "Item", "Quantity", "Price");
    Console.WriteLine(title);
    Console.WriteLine(separator);

    for (int i = 0; i< itemCount; i++) { 
        
        string itemRow = string.Format("{0,-15} | {1,10} | {2,-5:C}", itemName[i], itemQuantity[i], itemPrice[i]);

        Console.WriteLine(itemRow) ;
    }
    Console.WriteLine(separator);

    Console.WriteLine() ;
    CultureInfo culture = CultureInfo.CurrentCulture;
    Console.WriteLine(string.Format(culture, "Total quantity: {0:N0}", totalQuantity));
    Console.WriteLine(string.Format(culture, "Total value: {0:C}", totalValue));

}

void viewAllItems()
{
    int idR = 0;

    Console.WriteLine("Id".PadRight(10) +"Item".PadRight(20) + "Quantity".PadRight(10) + "Price".PadLeft(10));

    for (int i = 0; i < itemCount; i++)
    {
        idR++;
        string id = idR.ToString().PadRight(10);
        string item = itemName[i].Replace("-", "").PadRight(20);
        string quantity = itemQuantity[i].ToString().PadRight(10);

        CultureInfo culture = CultureInfo.CurrentCulture;

        string price = string.Format(culture, "{0,10:C}", itemPrice[i]);

        Console.WriteLine(id + item + quantity + price);
    }
}
