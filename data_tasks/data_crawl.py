# -*- coding: utf-8 -*-
"""
Spyder Editor

This is a temporary script file.
"""

import pandas as pd
import numpy as np
import os
df = pd.read_csv('G:/Data/train.csv')
#%%
df_products = df[['Product ID','Category','Sub-Category','Product Name']].drop_duplicates(subset=['Product ID'],keep='first')
#%%
os.chdir('G:/Data/Crawled')
import urllib
from selenium import webdriver
from time import sleep
from selenium.webdriver.common.by import By
driver = webdriver.Chrome()
result = []
for i in df_products['Product Name'].values:
    driver.get("https://google.com")
    sleep(3)
    search_bar = None
    while search_bar is None:
        try:
            search_bar = driver.find_element(By.CSS_SELECTOR,'#APjFqb')
            search_bar.send_keys('What is the price of ' + str(i) + " amazon")
        except:
            driver.get("https://google.com")
            sleep(3)
    sleep(1)
    search_bar.send_keys('\n')
    sleep(2)
    search_results = driver.find_elements(By.CSS_SELECTOR,'body div.main div div:nth-of-type(12) div div')
    for result_no ,temp in enumerate(search_results):
        try: 
            cite = temp.find_element(By.CSS_SELECTOR,'div div div div div div div div cite').text
            print(cite)
            if "amazon" in str(cite).lower():
                rating = temp.find_element(By.CSS_SELECTOR,'div div div div div:nth-of-type(4) div span:nth-of-type(2)').text
                no_rates = temp.find_element(By.CSS_SELECTOR,'div div div div div:nth-of-type(4) div span:nth-of-type(3)').text
                price = temp.find_element(By.CSS_SELECTOR,'div div div div div:nth-of-type(4) div span:nth-of-type(4)').text
                img_name = str(i) + '.png'
                try:
                    img_src = temp.find_element(By.CSS_SELECTOR,'div div div div div.LnCrMe div div a div img').get_attribute('src')
                    urllib.request.urlretrieve(img_src,img_name)
                    sleep(5)
                except:
                    img_name = ''
                    print('Failed to get img for ',i)
                result.append([i,rating,no_rates,price,img_name])
                print(len(result),len(df_products) - len(result))
                break
        except:
            if result_no == len(search_results)-1:
                result.append([i,'NA','NA','NA','NA'])
                print('NA')
            else:
                print('miss')
driver.close()
#%%
crawl_result = np.array(result)
crawl_df = pd.DataFrame(crawl_result,columns=['Product  Name','Rating','Number of rates','Price','Img'])
crawl_df.to_csv('Crawled_data.csv')
#%%
crawl_test_df = pd.read_csv('Crawled_data.csv')
#%%
nan_rows = crawl_test_df[crawl_test_df.isnull().any(axis=1)]
nan_rows.to_csv('nan_rows.csv')
#%%
clean_crawled_data = crawl_test_df.dropna(how='any')
clean_crawled_data.to_csv('clean_crawled_data.csv')
#%%
#%%
# /html/body/div[1]/div[3]/form/div[1]/div[1]/div[1]/div/div[2]/textarea
# document.querySelectorAll('body div.main div div:nth-of-type(12) div div')
# document.querySelector('body div.main div div:nth-of-type(12) div div[role=main] div div div div div div div div div div cite')
# document.querySelector('body div.main div div:nth-of-type(12) div div[role=main] div div div div div div div:nth-of-type(4) div span:nth-of-type(2)')
# document.querySelector('body div.main div div:nth-of-type(12) div div[role=main] div div div div div div div.LnCrMe div div a div img').src
#%% #rso > div:nth-child(1) > div > div > div.kb0PBd.cvP2Ce.LnCrMe