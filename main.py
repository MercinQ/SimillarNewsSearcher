from flask import Flask
import requests
import json
import requests
from bs4 import BeautifulSoup
from flask_restful import Api, Resource
from flask_cors import CORS

#Api inicializacion
app = Flask(__name__)
CORS(app)
api = Api(app)

#headers
headers = {
    'User-Agent' : 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36'
}
websiteLinks = " "


class Scrap_selected(Resource):
    
    #Apis get method
    def get(self, websiteLinks):
        
        #fetch data from json file
        f = open('data.json')
        data = json.load(f)
        check_urls = data["scrap_all"]["check_right_urls"]
        wideo_link = data["scrap_all"]["Wideo_link"]
        wiadomosc_link = data["scrap_all"]["Wiadomosc_link"]

        #Validation input from UI
        if(websiteLinks == "polsat"):
            URL_1 = data["scrap_all"]["URL_1"]
            find_1 = data["scrap_all"]["find_1"]
            find_all_1 = data["scrap_all"]["find_all_1"]
            content_class = data["scrap_all"]["content_class_polsat"]
            json_data = Scraping(URL_1,find_all_1,find_1,content_class,check_urls,wideo_link,wiadomosc_link)
            return (json_data.polsat_scrap())
        
        elif(websiteLinks == "tvn"):
            URL_1 = data["scrap_all"]["URL_3"]
            find_1 = data["scrap_all"]["find_3"]
            find_all_1 = data["scrap_all"]["find_all_3"]
            content_class = data["scrap_all"]["content_class_tvn24"]
            json_data = Scraping(URL_1,find_all_1,find_1,content_class,check_urls,wideo_link,wiadomosc_link)
            return(json_data.tvn24_scrap())
            
        elif(websiteLinks == "dziennik"):
            URL_1 = data["scrap_all"]["URL_2"]
            find_1 = data["scrap_all"]["find_2"]
            find_all_1 = data["scrap_all"]["find_all_2"]
            content_class = data["scrap_all"]["content_class_dziennik"]
            json_data = Scraping(URL_1,find_all_1,find_1,content_class,check_urls,wideo_link,wiadomosc_link)
            return(json_data.dziennik_scrap())

        #WebScraping itself
class Scraping:
    def __init__(self, URL_1, find_all_1, find_1, content_class, check_urls,wideo_link,wiadomosc_link):
        #basic scraping
        self.page = requests.get(URL_1,headers=headers)
        self.soup = BeautifulSoup(self.page.content, "html.parser")
        self.results = self.soup.find(class_=f'{find_1}')
        self.scraped = self.results.find_all( class_=f'{find_all_1}')
        
        #variables assigement
        self.content_class = content_class
        self.check_urls = check_urls
        self.wideo_link = wideo_link
        self.wiadomosc_link = wiadomosc_link
        self.page_content = []
        self.content_urls = []
        self.final_urls = []
        self.lenght = len(self.scraped)
        self.particular_page_content = []
        
        #taking out all titles that are longer then 30 characters
        for eliminate_invalid in self.scraped:
            if len(eliminate_invalid.text) > 30:
                self.page_content.append(eliminate_invalid.text)
        
    # Scarping data with input equal to "polsat"
    def polsat_scrap(self):
        websiteLinks = "Polsat News"
        
        #elimination of invalid scraps
        for link in self.results.find_all('a'):
            self.content_urls.append(link.get('href'))
            
        #elimiating duplicats
        unique_content = list(set(self.content_urls))
        
        #gather group of valid urls
        for filtred_urls in unique_content:
            wideo = filtred_urls.startswith(f'{self.wideo_link}')
            wiadomosc = filtred_urls.startswith(f'{self.wiadomosc_link}')
            if wideo == True:
                self.final_urls.append(filtred_urls)
            elif wiadomosc == True:
                self.final_urls.append(filtred_urls)
                
        #gather group of valid content of each url
        for content_url in self.final_urls:
            content_page = requests.get(content_url, headers=headers)
            content_soup = BeautifulSoup(content_page.content, "html.parser")
            content_results = content_soup.find("div", class_= f'{self.content_class}')   
            self.particular_page_content.append(content_results.text)
            
        #return data as json
        return {
                "collected_data":{
                    "Page": f"Wiadomości pochodzą z serwisu: {websiteLinks}",
                    "Title": self.page_content,
                    "Amount of articles": len(self.page_content),
                    "Urls": self.final_urls,
                    "Amount of urls": len(self.final_urls),
                    "Content": self.particular_page_content,
                }
        }
        
    def tvn24_scrap(self):
        websiteLinks = "TVN24"
        
        #elimination of invalid scraps 
        for link in self.results.find_all('a'):
            scrapido = link.find_all(class_ = f'{self.check_urls}')
            if len(scrapido) != 0:
                self.content_urls.append(link.get('href'))
                
        for content_url in self.content_urls:
            content_page = requests.get(content_url,headers=headers)
            content_soup = BeautifulSoup(content_page.content, "html.parser")
            content_results = content_soup.find("div", class_=f'{self.content_class}')   
            self.particular_page_content.append(content_results.text)
                
        return {
                "collected_data":{
                    "Page": f"Wiadomości pochodzą z serwisu: {websiteLinks}",
                    "Title": self.page_content,
                    "Amount of articles": len(self.page_content),
                    "Urls": self.content_urls,
                    "Amount of urls": len(self.content_urls),
                    "Content": self.particular_page_content,
                }
        }
    # Scarping data with input equal to "dziennik"
    def dziennik_scrap(self):
        websiteLinks = "Wiadomości Dziennik"
        
        #elimination of invalid scraps
        for link in self.results.find_all('a'):
                self.content_urls.append(link.get('href'))

        #gather group of valid content of each url
        for content_url in self.content_urls:
            content_page = requests.get(content_url,headers=headers)
            content_soup = BeautifulSoup(content_page.content, "html.parser")
            content_results = content_soup.find("div", class_=f'{self.content_class}')
            delete_newline = content_results.text.replace('\n', '')
            self.particular_page_content.append(delete_newline)
            
        #return data as json
        return {
                "collected_data":{
                    "Page": f"Wiadomości pochodzą z serwisu: {websiteLinks}",
                    "Title": self.page_content,
                    "Amount of articles": len(self.page_content),
                    "Urls": self.content_urls,
                    "Amount of urls": len(self.content_urls),
                    "Content": self.particular_page_content,
                }
        }

api.add_resource(Scrap_selected, "/main/GetNews/<string:websiteLinks>")

if __name__ == "__main__":
    app.run(debug=True)