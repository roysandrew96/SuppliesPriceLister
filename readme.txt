Notes relating to the completion of the Coding Challenge
========================================================

1. With regard to the parsing of JSON data I chose to the Newtonsoft Nuget package having used it in the past when working with JSON data.

2. With regard to the CSV data, there are a number of CSV loading packages available, which I would normally investigate, but given the time frames and relative simplicity of the file to import, I opted to use a simple mechanism to read and import the file.

3. The data files have been marked as "copy if newer" in the solution, which is why all of my "file opening" expressions are very simple minded. I mention this because clearly any "real world" file opening would need to take much greater care about file location and manage file path / existence more carefully.

