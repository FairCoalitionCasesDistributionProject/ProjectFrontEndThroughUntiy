# Welcome To The Fair Division Of Ministries In A Governmental Coalition Calculation System

## A web application which calculates a fair division of ministries between government coalition members.

The platform gets from each party of a potential coalition, its number of mandates and values for each ministry representing their interest in it as an input. The software returns a fair division of the ministries between the parties as an output according to the leximin criterion.

## 🚀 How It Works

Here you can try to build a coalition with any parties dividing any ministries you want and see how it works together.

### Step-by-Step Process:

1. **Enter the number of ministries** that you want to build your coalition on
2. **Give those ministries descriptions** (we need to know what we're dividing, right?)
3. **Specify the number of parties** in your coalition and their total mandates
4. **Provide party descriptions** and their preferences for each ministry
5. **Review the results** - a fair division calculated using advanced algorithms

> **Note:** The results are highly dependent on the inputs you use. Even a difference of a single point might change the division.

After receiving the results, you can copy a URL link which can restore the session for further use.

## 🧮 The Algorithm

The system is based on an implementation of a linear programming algorithm for fair division of objects between members with non-equal rights. The algorithm is based on a theorem that in every division problem between n players with equal rights, there exists a proportional division where at most n-1 shared objects.

The algorithm is from [Fairpy](https://github.com/erelsgl/fairpy) written by Erel Segal-Halevi.

![Algorithm Overview](Media/1.png)

This function gets the values matrix containing each party's values for each item. It sends to dominating allocation with bounded sharing of the values and a division that each party gets the number of mandates/sum of all the mandates. In this way, we guarantee that each party will get a fair share of their size.

![Integer Programming](Media/2.png)

Here we take an existing allocation and use integer programming to divide the items such that each party will get at least the values it got from the first allocation, however this time only m - 1 item will be shared between the parties.

## 💻 Usage

![Usage Interface](Media/3.png)

The frontend is completely written in Unity and meant to be used in a web browser using WebGL.

### For downloading and running in Unity editor

```bash
git clone https://github.com/FairCoalitionCasesDistributionProject/ProjectFrontEndThroughUntiy.git
```

Open the project folder through Unity Hub and run the project.

**Note:** The project was written in Editor Version 2020.3.30f1 on Windows 11 OS (So it might not work as expected in other environments).

## 🛡️ Security Features

This application has been updated with comprehensive security measures:

### ✅ Security Fixes Implemented:

- **XSS Protection:** Replaced deprecated `Application.ExternalEval()` with secure clipboard operations
- **Input Validation:** All user inputs are validated using safe parsing methods (`TryParse` instead of `Parse`)
- **HTTPS Communication:** All API endpoints use secure HTTPS connections
- **Error Handling:** Comprehensive try-catch blocks prevent crashes from malformed inputs
- **Bounds Checking:** Array operations are protected against out-of-bounds access
- **Phone Number Validation:** Fixed logic errors in input validation

### 🔒 Security Best Practices:

- Input sanitization and validation
- Secure communication protocols
- Proper error handling and logging
- Protection against common web vulnerabilities
- Safe clipboard operations for WebGL builds

## 🏗️ Technology Stack

![Technology Overview](Media/4.jpg)

- **Frontend:** Unity WebGL
- **Backend:** RESTful API (Heroku)
- **Algorithm:** Linear Programming (Fairpy)
- **Security:** HTTPS, Input Validation, XSS Protection
- **Data Format:** JSON
- **Build Target:** Web Browser

## 📋 Requirements

- Unity 2020.3.30f1 or later
- Windows 11 (recommended) or compatible OS
- Modern web browser with WebGL support
- Internet connection for API communication

## 🚀 Deployment

### WebGL Build
1. Open the project in Unity
2. Go to File → Build Settings
3. Select WebGL platform
4. Click "Build" and choose output directory
5. Deploy the built files to a web server

### Local Development
1. Clone the repository
2. Open in Unity Hub
3. Install required Unity version
4. Open the project and run in editor

## 🤝 Contributing

We welcome contributions! Please ensure that:
- All code follows Unity best practices
- Security measures are maintained
- Input validation is implemented for new features
- Error handling is comprehensive

## 📄 License

This project is open source. Please refer to the license file for details.

## 👥 About Us

![Team](Media/5.png)

This project was developed as part of research into fair division algorithms for governmental coalition building. The system provides a practical tool for understanding and implementing fair distribution of ministerial positions in coalition governments.

## 📞 Support

For issues, questions, or contributions, please:
1. Check existing issues in the repository
2. Create a new issue with detailed description
3. Include system information and error logs if applicable

---

**Last Updated:** December 2024  
**Version:** 2.0 (Security Enhanced)




