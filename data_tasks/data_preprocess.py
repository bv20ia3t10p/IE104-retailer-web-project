# -*- coding: utf-8 -*-
"""
Created on Sat Sep 23 11:05:07 2023

@author: beave
"""

import numpy as np
import pandas as pd
df = pd.read_csv('G:/Data/Crawled/clean_crawled_data.csv')
#%%
def is_float(element: any) -> bool:
    #If you expect None to be passed:
    if element is None: 
        return False
    try:
        float(element)
        return True
    except ValueError:
        return False
#%%
def is_int(element: any) -> bool:
    #If you expect None to be passed:
    if element is None: 
        return False
    try:
        int(element)
        return True
    except ValueError:
        return False

#%%
df_temp = df
valid_rating_rows = df[df['Rating'].apply(lambda x:
                                      is_float(x.split(' ')[-1].replace(',','.')))]
valid_rating_rows['Rating'] = valid_rating_rows['Rating'].apply(lambda x: float(x.split(' ')[-1].replace(',','.')))
#%%
valid_rating_rows['Number of rates'] = valid_rating_rows['Number of rates'].apply(lambda x: int(x.split(' ')[0].replace('.','')))
#%%
valid_rating_rows['Item Price'] = valid_rating_rows['Price'].apply(lambda x: x.split(' ')[0].replace('.','').replace(',','.'))
valid_rating_rows['Currency'] = valid_rating_rows['Price'].apply(lambda x: x.split(' ')[1])
processed_data = valid_rating_rows[['Product  Name','Rating','Number of rates','Item Price','Currency']]
processed_data.to_csv('G:/Data/Crawled/processed_data.csv',index=False)
#%%
read_test = pd.read_csv('G:/Data/Crawled/processed_data.csv')
#%%
product_names = read_test['Product  Name'].values.astype(str)
df2 = pd.read_csv('G:/Data/train.csv')
df2_filtered = df2[df2['Product Name'].isin(product_names)]
df2_filtered['Price'] = df2_filtered['Product Name'].apply(lambda x: read_test[read_test['Product  Name'] == x]['Item Price'].values[0])
df2_filtered['Rating'] = df2_filtered['Product Name'].apply(lambda x: read_test[read_test['Product  Name'] == x]['Rating'].values[0])
df2_filtered['Currency'] = df2_filtered['Product Name'].apply(lambda x: read_test[read_test['Product  Name'] == x]['Currency'].values[0])
df2_filtered['Number of rates'] = df2_filtered['Product Name'].apply(lambda x: read_test[read_test['Product  Name'] == x]['Number of rates'].values[0])
