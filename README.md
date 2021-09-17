# MDMAKE

A command-line utility that reads source-code files (in C#, PHP, etc.) and generates a consolidated markdown file (README.md) from its comments. The purpose of this project is to create a minimum documentation on the project by leveraging some of the existing comments that we, the developers, often leave on the source code. 

I had in mind the following principles:
1. Multiple programming languages support; 
2. Some documentation is better than none;
3. Generated, dynamic documentation is better than static (because it will be outdated soon);
4. For some (or most) projects, what matters is the README.md. Ain't nobody got time to read dozens of Help Pages;
5. I wanted a single and simple way to generate the docs. This is why I only support single-line comments. Ain't nobody got time to deal with complex grammar processing of multi-line/block comments;

## Usage

Just add an exclamation mark (!) to the single line comments that you would like to output to the consolidated README.md file. Then, run the CLI (mdmake.exe).

Example:
```
//! This will be outputed to the README.md file and will be eternized in glory
// ! This will NOT be outputed
```

There is a compiled and ready to go version of the CLI inside the dist folder. 

```
C:\git\mdmake\dist>mdmake.exe -i ./source -h ./docs/header.md -o ./README.md
```

## Supported languages
* C#
* PHP (soon)


