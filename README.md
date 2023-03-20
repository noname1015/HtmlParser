# HtmlParser
C# program to convert html tables to HTML format
This coding is just for own study purpose

Class: HtmlContentParser

Description:
This class is responsible for parsing HTML table content and converting it into properly indented and formatted HTML code. It contains a static method "ParseTable" which takes a string input and returns a string output.

Methods:

ParseTable(string input): This method takes a string input containing the HTML table content and parses it to create a properly indented and formatted HTML code. It returns a string output.

Class: HtmlObjLine

Description:
This class represents an HTML object line and contains information about the tag and its level in the hierarchy.

Properties:

Tag (string): The name of the HTML tag.
Level (int): The level of the HTML tag in the hierarchy.

Code:

The code starts with the definition of the Form1 class which is a Windows Form class that contains a single button "btnConvert" and two text boxes "txtInput" and "txtOutput". The "btnConvert_Click" event handler is responsible for invoking the "ParseTable" method of the HtmlContentParser class and displaying the output in the "txtOutput" text box.

The HtmlContentParser class contains a static method "ParseTable" which takes the input HTML table content and parses it into properly indented and formatted HTML code. The method uses a stack data structure to keep track of the hierarchy of HTML tags and their levels. The method iterates through each line of the input content and determines the tag name and its level based on the indentation. It then compares the current tag's level with the previous tag's level to determine whether to add a new tag, close a tag, or close multiple tags. The method then returns the properly indented and formatted HTML code.
