# 📦 File Bundler CLI

## 📘 Overview
This is a command-line interface (CLI) tool for bundling multiple code files into a single text file. It helps organize and package files written in different programming languages into one unified and readable output.

---

## ⚙️ Parameters

- `--language <languages>`: Comma-separated list of languages to include (e.g., `js,py`)  
- `--output <output_file>`: Name of the output file  
- `--note [true|false]`: Whether to include notes   
- `--sort [true|false]`: Whether to sort files before bundling   
- `--remove [true|false]`: Remove empty lines from the output 
- `--author <author_name>`: Add author name to the output

---

## ▶️ Example

```bash
 b --o ff --l all --n true --s true --r true --a miri```

---

## 🔄 Bundling Command Format

```bash
fib bundle --language <languages split with ","> --output <output_file> --note [true|false] --sort [true|false] --remove [true|false] --author <author_name>
```

---

## 📄 License

MIT (or specify another if applicable)
