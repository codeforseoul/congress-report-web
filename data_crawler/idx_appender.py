import json

from datetime import datetime
from idx_finder import *

assembly_data = json.load(open('assembly.json', encoding='utf-8'))


def append_idxs(source_dataset):
    result_dataset = []
    count = len(source_dataset)
    i = 0
    for member in source_dataset:
        name = member['name_kr']
        birthday = member['birth']
        birthday_date = datetime.strptime(birthday, '%Y-%m-%d').date()

        try:
            popong_idx = find_popong_idx(name, birthday_date)
            assembly_idx = find_assembly_idx(name, birthday_date)
            open_assembly_idx = find_open_assembly_idx(name, birthday_date)
        except:
            print('%s has error' % name)

        result_data = member.copy()
        result_data['popong_idx'] = popong_idx
        result_data['assembly_idx'] = assembly_idx
        result_data['open_assembly_idx'] = open_assembly_idx

        result_dataset.append(result_data)

        i += 1
        print('%d of %d processed.' % (i, count))
    return result_dataset

if __name__ == '__main__':
    modified_data = append_idxs(assembly_data)
    json.dump(modified_data, open(
        'modified_assembly.json', 'w', encoding='utf-8'))
    print('complete')
