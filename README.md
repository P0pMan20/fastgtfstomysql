# A fast GTFS-Static to MySQL importer

## Key Optimisations

- Multithreading
- InnoDB does a COMMIT for each INSERT as soon as it is received by [default](https://dev.mysql.com/doc/refman/5.7/en/optimizing-innodb-bulk-data-loading.html), so by wrapping the set of inserts in a TRANSACTION the overhead of doing a log flush to disk for every insert is avoided.

The Cairns and South East Queensland GTFS schedule [found here](https://translink.com.au/about-translink/open-data/gtfs-rt) is used for testing.

~60x faster than the reference implementation provided by my teacher (derived from [this repo](https://github.com/steffenz/php-gtfs-mysql)) when inserting a truncated GTFS file (stop_times_trunc.txt)
(The reference takes over 2 hours to do the full file)

#### My Implementation
~5 seconds

![img](https://github.com/user-attachments/assets/d6927948-aef2-496a-b236-413a08b88217)

### Example implementation
~300 seconds

![img_1](https://github.com/user-attachments/assets/dc13a93d-b5d3-409a-a030-1d0072b26bec)

## Remaining Optimisations/Features
- [ ] Resolve all TODOs
- [ ] Add support for *All* files listed in the GTFS spec rather than just the required, conditionally required and those present in the Translink GTFS spec
- [ ] Optimise memory usage
- [ ] Add CLI and support for arguments (ie SQL connection string)
- [ ] Refactor so GTFSParser doesn't take in a file path, it should take in a file or stream? 
