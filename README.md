# Software Testing (C1TIIB) Autumn 2024
This is the repository for the Softwre Testing (Autumn 2024) course at Borås University.

The course schedule can be found on [Kronox](https://schema.hb.se/setup/jsp/Schema.jsp?startDatum=2024-08-01&intervallTyp=m&intervallAntal=6&sprak=SV&sokMedAND=true&forklaringar=true&resurser=k.C1TIIB-20242-IN83H-) and the course material can be found on [Canvas](https://hb.instructure.com/courses/8242).

## Delopment Environment Setup

First, make sure you have installed Visual Studio Code, Miniconda, Git, and the .NET Sdk on your computer.

### Software

Install the following software on your computer:

  -  [Visual Studio Code](https://code.visualstudio.com)
  -  [Miniconda](https://docs.anaconda.com/miniconda/install/#quick-command-line-install)
  -  [Git](https://git-scm.com/downloads)
  -  [.NET Sdk](https://dotnet.microsoft.com/en-us/download) (install .net 9.0)

### Verify the Software Installation
Verify the software has been successfully installed, by opening a terminal and issuing the following commands (each command should print out a version):

  - `code --version`
  - `conda --version`
  - `git --version`
  - `dotnet --version`

If you don't see the print-out of a version for a specific tool, make sure the path to your tool has been added to your `PATH`environment variable. Also, run the command below to verify you have installed both versiopns of the .net sdk (.net 9.0 and .net 8.0).

- `dotnet --list-sdks`

## Course Repository

When you have installed the software above, open a terminal and clone the GitHub repository [C1TIIB_2024](https://github.com/paga-hb/C1TIIB_2024) to your computer, and create a Python environment:

- `git clone https://github.com/paga-hb/C1TIIB_2024.git testing`
- `cd testing`
- `conda create -y -p ./.conda python=3.10`
- `conda activate ./.conda`
- `python -m pip install --upgrade pip`
- `pip install ipykernel jupyter`

## Visual Studio Code (VSCode) Extensions

Then install the necessary Visual Studio Code Extensions by executing the commands below in your terminal:

- `code --install-extension ms-dotnettools.csdevkit`
- `code --install-extension ms-dotnettools.vscodeintellicode-csharp`
- `code --install-extension ms-toolsai.jupyter`
- `code --install-extension yzhang.markdown-all-in-one`
- `code --install-extension ms-python.python`
- `code --install-extension humao.rest-client`
- `code --install-extension ms-mssql.mssql`
- `code --install-extension alexcvzz.vscode-sqlite`

## Open the First Workshop Notebook

Finally, make sure you are in the `testing` folder in your terminal, and open the first notebook in Visual Studio Code, by issuing the command below:

- `code -g notebooks/vscode.ipynb:0 .`

When the notebook opens in VSCode, click the text `Select Kernel` (in the top-right of the notebook), and choose `Python Environments... => conda (Python 3.10.15) .conda/bin/python`.

Now you can follow the instructions in the notebook.