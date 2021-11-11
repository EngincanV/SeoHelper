# SeoHelper

[![CodeFactor](https://www.codefactor.io/repository/github/engincanv/seohelper/badge/main)](https://www.codefactor.io/repository/github/engincanv/seohelper/overview/main)
[![NuGet](https://img.shields.io/nuget/v/SeoHelper.svg?style=flat-square)](https://www.nuget.org/packages/SeoHelper)

This package helps you to add meta-tags, sitemap.xml and robots.txt into your project easily.

## Usage

1. Install the package via Nuget.
```sh
Install-Package SeoHelper -Version 2.0.0
```
or

```sh
dotnet add package SeoHelper --version 2.0.0
```

2. Add following service registrations to `ConfigureServices` method in **Startup.cs**.
> Note: You can define your seo options (meta-tags, sitemap.xml and robots.txt) either specifying them in `SeoOptions` class or appsettings.json (under a section. E.g: SeoOptions or SeoConfigurations).

```sh
services.AddSeo(Configuration, sectionName: "SeoOptions");
```
> If you want to specify your options via appsettings.json. You can create a section named whatever you want ("SeoOptions" in above usage) and specify it in the related service configuration method (**AddSeo**).

```json
"SeoOptions": {
    "MetaTags": [
      { 
        "RelativeUrl": "/demo",
        "Title": "demo title",
        "Charset": "UTF-8",
        "MetaTagDescriptions": {
          "Author": "author",
          "Description": "description",
          "Keywords": "keywords"
        }
      },
      {
        "RelativeUrl": "/",
        "Title": "index title",
        "Charset": "UTF-8",
        "MetaTagDescriptions": {
          "author": "author",
          "description": "description"
        }
      }
    ],
    "Sitemap": {
      "Urls": [
        {
          "Url": "/article/1",
          "LastModificationDate": "12/12/2012"
        },
        {
          "Url": "/article/2",
          "LastModificationDate": "12/12/2012",
          "Priority": 1.0
        }
      ]
    },
    "RobotsTxt": {
      "DisplaySitemapUrl": true,
      "RobotsTxtSections": [
        {
          "UserAgent": "/google",
          "DisallowUrls": ["/account/manage", "/account/login"],
          "AllowUrls": ["/", "/home-page"]
        },
        {
          "UserAgent": "/yandex",
          "DisallowUrls": ["/account/manage", "/account/login"],
          "AllowUrls": ["*"]
        }
      ]
    },
    "OpenGraph": {
      "Twitter": {
        "Site": "@EngincanVeske",
        "Creator": "@EngincanVeske"
      },
      "Pages": [
        {
          "Url": "/",
          "OgTitle": "og-title",
          "OgType": "og-type",
          "OgImage": "og-image.jpg",
          "OgUrl": "og-url.com"
        },
        {
          "Url": "/demo",
          "OgTitle": "og-title2",
          "OgType": "og-typ2e",
          "OgImage": "og-image2.jpg",
          "OgUrl": "og-url2.com"
        }
      ]
    }
  },
```

> You can configure your options like below in **appsettings.json**. With v2.0.0 now you can configure your open-graph and twitter card tags as well.

3. Add following custom middleware to your `Configure` method in **Startup.cs**:

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    //...
    app.UseSeoHelper();
    //...
}
```

> If you encounter a problem, you can examine the sample ([**SeoHelper.Demo**](https://github.com/EngincanV/SeoHelper/tree/main/samples/SeoHelper.Demo)) in this repo.

---

## Roadmap

* **Version 1**
- [X] Meta-tags
- [X] Sitemap.xml
- [X] Robots.txt

* **Version 2**
- [X] OpenGraph tags (for social media accounts)
- [X] Bug-fixes
