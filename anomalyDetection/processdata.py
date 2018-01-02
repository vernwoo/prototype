from pandas import DataFrame
from datetime import datetime

def truncate2minute(dt):
    return datetime(dt.year, dt.month, dt.day, dt.hour, dt.minute)


data = DataFrame.from_csv('2017.csv')
data['count'] = 1
data['datetime'] = data['datetime'].apply(dateutil.parser.parse)
data['datetime'] = data['datetime'].apply(truncate2min)
data['count'] = data.groupby(['datetime', 'upn', 'perm', 'result']).transform('sum')
data = data.drop_duplicates()
data.to_csv('2017_byminute.csv', encoding='utf-8')

